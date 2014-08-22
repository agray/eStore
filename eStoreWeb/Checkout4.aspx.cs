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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web;
using com.phoenixconsulting.culture;
using eStoreBLL;
using eStoreWeb.Controls;
using phoenixconsulting.businessentities.list;
using phoenixconsulting.common.basepages;
using PhoenixConsulting.Common.Enums.Logging;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.logging;
using phoenixconsulting.common.navigation;
using phoenixconsulting.paymentservice.creditcard;
using phoenixconsulting.paymentservice.paypal;
using PhoenixConsulting.PaymentService.PayPal.com.paypal.sandbox.www;

namespace eStoreWeb {
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
    public partial class Checkout4 : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            CameFrom.HandleNavigationRedirect(Request.UrlReferrer);
            
            //string redirectPage = NavigationUtils.getInvalidNavigationRedirect(Request.UrlReferrer);
            //if(!redirectPage.Equals("")) {
            //    Response.Redirect(redirectPage);
            //}
            
            //CultureService.SetCulture(SessionHandler.Instance.CurrencyValue);

            if(Request.QueryString["token"] != null) {
                PayPalApiUtil.GetExpressCheckoutDetails(Request, Page);
            }
        }

        protected void GoToPreviousPage(object sender, EventArgs e) {
            GoTo.Instance.Checkout3Page();
        }

        protected void PlaceOrder(object sender, EventArgs e) {
            var orderId = SaveOrderToDatabase();
            if(orderId == -1) {
                //Order did not get saved to the database correctly
                //don't bother going to CC Processor
                GoTo.Instance.OrderProcessingErrorPage();
            } else {
                if(Request.QueryString["token"] != null) {
                    ProcessPayPalPayment(orderId);
                } else {
                    //Process CC Payment
                    //Submit CC Details to SecurePay for authorisation and update status of Order
                    var ccProcessor = new CreditCardPaymentProcessor();
                    ccProcessor.processPayment(orderId, HttpContext.Current.Request);
                    GoTo.Instance.Checkout5Page();
                    //Response.Redirect("ShowMe.aspx");
                }
            }        
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private void ProcessPayPalPayment(int orderId) {
            //string token = Request.QueryString["token"];
            //Submit Details to PayPal for completion
            var finalResponse = PayPalApiUtil.CompletePayPalTransaction();
            var paymentInfo = finalResponse.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0];

            UpdateOrder(orderId, paymentInfo, finalResponse);

            if(IsPpApproved(paymentInfo.PaymentStatus.ToString())) {
                //Order Approved by PayPal. Update order in database.
                SessionHandler.Instance.IsPaymentApproved = "Yes";
                SessionHandler.Instance.PaymentResponseCode = "00";
                LoggerUtil.AuditLog(NLog.LogLevel.Info,
                                AuditEventType.SUBMIT_PAYPAL_PAYMENT_SUCCESS,
                                "StoreSite",
                                null,
                                null, null, null, null, null);
            } else {
                //PayPal payment declined. Update order in database.
                SessionHandler.Instance.IsPaymentApproved = "No";
                SessionHandler.Instance.PaymentResponseCode = paymentInfo.ReasonCode.ToString();
                LoggerUtil.AuditLog(NLog.LogLevel.Info,
                                AuditEventType.SUBMIT_PAYPAL_PAYMENT_REJECTED,
                                "StoreSite",
                                null,
                                null, null, null, null, null);
            }
            GoTo.Instance.Checkout5Page();
        }

        private static void UpdateOrder(int orderId, 
                                        PaymentInfoType paymentInfo, 
                                        AbstractResponseType finalResponse) {
            var order = new OrdersBLL();
            order.updateOrderStatus(orderId,
                                    paymentInfo.ReasonCode.ToString() != "none" 
                                        ? Convert.ToInt32(paymentInfo.ReasonCode.ToString()) 
                                        : 0,
                                    paymentInfo.TransactionID,
                                    paymentInfo.PaymentDate.ToString(CultureInfo.InvariantCulture),
                                    finalResponse.Ack.ToString(),
                                    finalResponse.CorrelationID,
                                    finalResponse.Timestamp.ToString(CultureInfo.InvariantCulture),
                                    CultureService.getLocalCultureDouble(paymentInfo.FeeAmount.Value),
                                    paymentInfo.PaymentStatus.ToString(),
                                    paymentInfo.ReasonCode.ToString(),
                                    paymentInfo.PaymentDate.ToString(CultureInfo.InvariantCulture));
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private static bool IsPpApproved(string paymentStatus) {
            return paymentStatus.Equals("Completed");
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private int SaveOrderToDatabase() {
            var userName = Page.User.Identity.Name;
            Guid userId;
            var order = new OrdersBLL();
            var orderDetailXml = CreateOrderDetailXml();
            
            var shoppingCart = SessionHandler.Instance.ShoppingCart;
            if(shoppingCart != null && false | shoppingCart.Count == 0 | !IsAllDataPopulated()) {
                return -1;
            }

            if(string.IsNullOrEmpty(userName)) {
                //user not logged in, try to find it by email
                var userBll = new UserBLL();
                var foundUserId = userBll.GetUserIdByEmail(SessionHandler.Instance.BillingEmailAddress, "eStore");
                userId = new Guid(foundUserId);
            } else {
                //user is logged in
                userId = BaseUserControl.GetUserId(this);
                //DTMembershipProvider dtmp = (DTMembershipProvider)Membership.Providers["DTMembershipProvider"];
                //userID = dtmp.getLoggedInUserID(Page.User.Identity.Name);
            }
            var orderId = order.AddOrder(SessionHandler.Instance.BillingEmailAddress,
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
                                         SessionHandler.Instance.TotalShipping,
                                         orderDetailXml.Replace(",", "."),
                                         CommentTextBox.Text,
                                         GiftTagMessageTextBox.Text,
                                         SessionHandler.Instance.TotalCost,
                                         userId);
            return orderId;
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
                     String.IsNullOrEmpty(SessionHandler.Instance.TotalShipping.ToString(CultureInfo.InvariantCulture)));
        }

        //
        //Returns an Order Detail XML document
        //
        private static string CreateOrderDetailXml() {
            //Header
            var tempXml = "<root>";
            var shoppingCart = SessionHandler.Instance.ShoppingCart;

            //Loop through Order Detail Records
            foreach(DTItem cartItem in shoppingCart) {
                var prodId = cartItem.ProductId;
                var quantity = cartItem.ProductQuantity;
                var unitPrice = cartItem.ProductOnSale == 1 ? cartItem.ProductDiscountPrice : cartItem.ProductPrice;
                var weight = cartItem.ProductWeight;
                var colorId = cartItem.ColorId;
                var sizeId = cartItem.SizeId;
                var catId = cartItem.CategoryId;
                var deptId = cartItem.DepartmentId;

                tempXml = tempXml + "<OrderDetail OrderID=\"" +
                                    "REPLACEME" + "\"" +
                                    " ProductID=\"" + prodId + "\"" +
                                    " Quantity=\"" + quantity + "\"" +
                                    " UnitPrice=\"" + unitPrice + "\"" +
                                    " ProductWeight=\"" + weight + "\"" +
                                    " ColorID=\"" + colorId + "\"" +
                                    " SizeID=\"" + sizeId + "\"" +
                                    " CategoryID=\"" + catId + "\"" +
                                    " DepartmentID=\"" + deptId + "\"" + "/>";
            }
            //Footer
            tempXml = tempXml + "</root>";
            return tempXml;
        }
    }
}