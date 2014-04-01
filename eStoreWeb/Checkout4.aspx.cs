#region Licence
/*
 * The MIT License
 *
 * Copyright (c) 2008-2013, Andrew Gray
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using com.phoenixconsulting.culture;
using eStoreBLL;
using eStoreWeb.Controls;
using phoenixconsulting.businessentities.list;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.logging;
using phoenixconsulting.common.navigation;
using phoenixconsulting.paymentservice.creditcard;
using phoenixconsulting.paymentservice.paypal;
using PhoenixConsulting.PaymentService.PayPal.com.paypal.sandbox.www;

namespace eStoreWeb {
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
    public partial class checkout4 : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            CameFrom.handleNavigationRedirect(Request.UrlReferrer);
            
            //string redirectPage = NavigationUtils.getInvalidNavigationRedirect(Request.UrlReferrer);
            //if(!redirectPage.Equals("")) {
            //    Response.Redirect(redirectPage);
            //}
            
            //CultureService.SetCulture(SessionHandler.Instance.CurrencyValue);

            if(Request.QueryString["token"] != null) {
                PayPalAPIUtil.GetExpressCheckoutDetails(Request, Page);
            }
        }

        protected void GoToPreviousPage(object sender, EventArgs e) {
            GoTo.Instance.Checkout3Page();
        }

        protected void PlaceOrder(object sender, EventArgs e) {
            int orderID = saveOrderToDatabase();
            if(orderID == -1) {
                //Order did not get saved to the database correctly
                //don't bother going to CC Processor
                GoTo.Instance.OrderProcessingErrorPage();
            } else {
                if(Request.QueryString["token"] != null) {
                    ProcessPayPalPayment(orderID);
                } else {
                    //Process CC Payment
                    //Submit CC Details to SecurePay for authorisation and update status of Order
                    CreditCardPaymentProcessor ccProcessor = new CreditCardPaymentProcessor();
                    ccProcessor.processPayment(orderID, HttpContext.Current.Request);
                    GoTo.Instance.Checkout5Page();
                    //Response.Redirect("ShowMe.aspx");
                }
            }        
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private void ProcessPayPalPayment(int orderID) {
            //string token = Request.QueryString["token"];
            //Submit Details to PayPal for completion
            DoExpressCheckoutPaymentResponseType finalResponse = PayPalAPIUtil.CompletePayPalTransaction();
            PaymentInfoType paymentInfo = finalResponse.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0];

            updateOrder(orderID, paymentInfo, finalResponse);

            if(isPPApproved(paymentInfo.PaymentStatus.ToString())) {
                //Order Approved by PayPal. Update order in database.
                SessionHandler.Instance.IsPaymentApproved = "Yes";
                SessionHandler.Instance.PaymentResponseCode = "00";
                LoggerUtil.auditLog(NLog.LogLevel.Info,
                                AuditEventType.SUBMIT_PAYPAL_PAYMENT_SUCCESS,
                                "StoreSite",
                                null,
                                null, null, null, null, null);
            } else {
                //PayPal payment declined. Update order in database.
                SessionHandler.Instance.IsPaymentApproved = "No";
                SessionHandler.Instance.PaymentResponseCode = paymentInfo.ReasonCode.ToString();
                LoggerUtil.auditLog(NLog.LogLevel.Info,
                                AuditEventType.SUBMIT_PAYPAL_PAYMENT_REJECTED,
                                "StoreSite",
                                null,
                                null, null, null, null, null);
            }
            GoTo.Instance.Checkout5Page();
        }

        private void updateOrder(int orderID, 
                                 PaymentInfoType paymentInfo, 
                                 DoExpressCheckoutPaymentResponseType finalResponse) {
            OrdersBLL order = new OrdersBLL();
            if(paymentInfo.ReasonCode.ToString() != "none") {
                order.updateOrderStatus(orderID,
                                        Convert.ToInt32(paymentInfo.ReasonCode.ToString()),
                                        paymentInfo.TransactionID,
                                        paymentInfo.PaymentDate.ToString(),
                                        finalResponse.Ack.ToString(),
                                        finalResponse.CorrelationID,
                                        finalResponse.Timestamp.ToString(),
                                        CultureService.getLocalCultureDouble(paymentInfo.FeeAmount.Value),
                                        paymentInfo.PaymentStatus.ToString(),
                                        paymentInfo.ReasonCode.ToString(),
                                        paymentInfo.PaymentDate.ToString());
            } else {
                order.updateOrderStatus(orderID,
                                        0,
                                        paymentInfo.TransactionID,
                                        paymentInfo.PaymentDate.ToString(),
                                        finalResponse.Ack.ToString(),
                                        finalResponse.CorrelationID,
                                        finalResponse.Timestamp.ToString(),
                                        CultureService.getLocalCultureDouble(paymentInfo.FeeAmount.Value),
                                        paymentInfo.PaymentStatus.ToString(),
                                        paymentInfo.ReasonCode.ToString(),
                                        paymentInfo.PaymentDate.ToString());
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private bool isPPApproved(string paymentStatus) {
            return paymentStatus.Equals("Completed");
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private int saveOrderToDatabase() {
            string userName = Page.User.Identity.Name;
            Guid userID;
            OrdersBLL order = new OrdersBLL();
            ArrayList shoppingCart = default(ArrayList);
            string orderDetailXML = CreateOrderDetailXML();
            
            shoppingCart = SessionHandler.Instance.ShoppingCart;
            if(shoppingCart == null | shoppingCart.Count == 0 | !IsAllDataPopulated()) {
                return -1;
            }

            if(string.IsNullOrEmpty(userName)) {
                //user not logged in, try to find it by email
                UserBLL userBLL = new UserBLL();
                string foundUserID = userBLL.getUserIDByEmail(SessionHandler.Instance.BillingEmailAddress, "eStore");
                userID = new Guid(foundUserID);
            } else {
                //user is logged in
                userID = BaseUserControl.getUserID(this);
                //DTMembershipProvider dtmp = (DTMembershipProvider)Membership.Providers["DTMembershipProvider"];
                //userID = dtmp.getLoggedInUserID(Page.User.Identity.Name);
            }
            int orderID = order.addOrder(SessionHandler.Instance.BillingEmailAddress,
                                         SessionHandler.Instance.BillingFirstName,
                                         SessionHandler.Instance.BillingLastName,
                                         SessionHandler.Instance.BillingAddress,
                                         SessionHandler.Instance.BillingCitySuburb,
                                         SessionHandler.Instance.BillingStateRegion,
                                         SessionHandler.Instance.BillingPostcode,
                                         Int32.Parse(SessionHandler.Instance.BillingCountry),
                                         SessionHandler.Instance.ShippingFirstName,
                                         SessionHandler.Instance.ShippingLastName,
                                         SessionHandler.Instance.ShippingAddress,
                                         SessionHandler.Instance.ShippingCitySuburb,
                                         SessionHandler.Instance.ShippingStateRegion,
                                         SessionHandler.Instance.ShippingPostcode,
                                         Int32.Parse(SessionHandler.Instance.ShippingCountry),
                                         Int32.Parse(SessionHandler.Instance.ShippingMode),
                                         (double)SessionHandler.Instance.TotalShipping,
                                         orderDetailXML.Replace(",", "."),
                                         CommentTextBox.Text,
                                         GiftTagMessageTextBox.Text,
                                         SessionHandler.Instance.TotalCost,
                                         userID);
            return orderID;
        }

        //
        //Checks to see if all session variables still have values
        //
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private static bool IsAllDataPopulated()
        {
            return !(String.IsNullOrEmpty(SessionHandler.Instance.BillingEmailAddress) |
                     String.IsNullOrEmpty(SessionHandler.Instance.BillingFirstName) |
                     String.IsNullOrEmpty(SessionHandler.Instance.BillingLastName) |
                     String.IsNullOrEmpty(SessionHandler.Instance.BillingAddress) |
                     String.IsNullOrEmpty(SessionHandler.Instance.BillingCitySuburb) |
                     String.IsNullOrEmpty(SessionHandler.Instance.BillingStateRegion) |
                     String.IsNullOrEmpty(SessionHandler.Instance.BillingPostcode) |
                     String.IsNullOrEmpty(SessionHandler.Instance.BillingCountry) |
                     String.IsNullOrEmpty(SessionHandler.Instance.ShippingFirstName) |
                     String.IsNullOrEmpty(SessionHandler.Instance.ShippingLastName) |
                     String.IsNullOrEmpty(SessionHandler.Instance.ShippingAddress) |
                     String.IsNullOrEmpty(SessionHandler.Instance.ShippingCitySuburb) |
                     String.IsNullOrEmpty(SessionHandler.Instance.ShippingStateRegion) |
                     String.IsNullOrEmpty(SessionHandler.Instance.ShippingPostcode) |
                     String.IsNullOrEmpty(SessionHandler.Instance.ShippingCountry) |
                     String.IsNullOrEmpty(SessionHandler.Instance.ShippingMode) |
                     String.IsNullOrEmpty(SessionHandler.Instance.TotalShipping.ToString()));
        }

        //
        //Returns an Order Detail XML document
        //
        private static string CreateOrderDetailXML() {
            //Header
            string tempXML = "<root>";
            ArrayList shoppingCart = SessionHandler.Instance.ShoppingCart;

            //Loop through Order Detail Records
            foreach(DTItem cartItem in shoppingCart) {
                int ProdID = cartItem.ProductId;
                int Quantity = cartItem.ProductQuantity;
                double UnitPrice = cartItem.ProductOnSale == 1 ? cartItem.ProductDiscountPrice : cartItem.ProductPrice;
                double Weight = cartItem.ProductWeight;
                int ColorID = cartItem.ColorId;
                int SizeID = cartItem.SizeId;
                int CatID = cartItem.CategoryId;
                int DeptID = cartItem.DepartmentId;

                tempXML = tempXML + "<OrderDetail OrderID=\"" +
                                    "REPLACEME" + "\"" +
                                    " ProductID=\"" + ProdID + "\"" +
                                    " Quantity=\"" + Quantity + "\"" +
                                    " UnitPrice=\"" + UnitPrice + "\"" +
                                    " ProductWeight=\"" + Weight + "\"" +
                                    " ColorID=\"" + ColorID + "\"" +
                                    " SizeID=\"" + SizeID + "\"" +
                                    " CategoryID=\"" + CatID + "\"" +
                                    " DepartmentID=\"" + DeptID + "\"" + "/>";
            }
            //Footer
            tempXML = tempXML + "</root>";
            return tempXML;
        }
    }
}