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
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.UI;
using com.phoenixconsulting.culture;
using com.phoenixconsulting.exceptions;
using NLog;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.logging;
using PhoenixConsulting.PaymentService.PayPal.com.paypal.sandbox.www;

namespace phoenixconsulting.paymentservice.paypal {
    public class PayPalApiUtil {
        #region "PaymentMethods"
        //****************************
        //Payment Methods
        //****************************
        public static void SetupExpressCheckout(HttpRequest request, HttpResponse response) {
            //build request
            var reqDetails = new SetExpressCheckoutRequestDetailsType();
            var basePath = request.Url.AbsoluteUri.Replace(request.Url.PathAndQuery, 
                                                              string.Empty) + 
                                                              request.ApplicationPath;

            reqDetails.ReturnURL = basePath + "Checkout4.aspx";
            reqDetails.CancelURL = basePath + "Checkout3.aspx";
            reqDetails.NoShipping = "1";
            reqDetails.OrderTotal = new BasicAmountType {
                currencyID = CurrencyCodeType.AUD,
                Value = CultureService.toInternalLocalCulture(SessionHandler.Instance.TotalCost),
            };

            var req = new SetExpressCheckoutReq {
                SetExpressCheckoutRequest = new SetExpressCheckoutRequestType {
                    Version = ApplicationHandler.Instance.PaypalVersion,
                    SetExpressCheckoutRequestDetails = reqDetails
                }
            };

            //query PayPal and get token
            var resp = BuildPayPalWebservice().SetExpressCheckout(req);
            HandleError(resp);

            //redirect user to PayPal
            response.Redirect(string.Format("{0}?cmd=_express-checkout&token={1}",
                                            ApplicationHandler.Instance.PaypalAPIURL, resp.Token));
        }

        public static void GetExpressCheckoutDetails(HttpRequest request, Page page) {
            if(!page.IsPostBack) {
                var token = request.QueryString["token"];

                //build getdetails request
                var req = new GetExpressCheckoutDetailsReq {
                    GetExpressCheckoutDetailsRequest = new GetExpressCheckoutDetailsRequestType {
                        Version = ApplicationHandler.Instance.PaypalVersion,
                        Token = token
                    }
                };

                //query PayPal for transaction details
                var resp =
                    BuildPayPalWebservice().GetExpressCheckoutDetails(req);
                HandleError(resp);

                var respDetails = resp.GetExpressCheckoutDetailsResponseDetails;

                //ShippingAddress
                //respDetails.PayerInfo.Address

                SessionHandler.Instance.PPCheckoutData = resp;
                SessionHandler.Instance.PPCheckoutDetails = respDetails;
            }
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public static DoExpressCheckoutPaymentResponseType CompletePayPalTransaction() {
            //get transaction details
            var resp = SessionHandler.Instance.PPCheckoutData;
            var respDetails = SessionHandler.Instance.PPCheckoutDetails;

            //create a new object, because it is now an array !!!!
            var sPaymentDetails = new PaymentDetailsType[1];
            sPaymentDetails[0] = new PaymentDetailsType {
                OrderTotal = new BasicAmountType {
                    currencyID = respDetails.PaymentDetails[0].OrderTotal.currencyID,
                    Value = respDetails.PaymentDetails[0].OrderTotal.Value
                },
                //must specify PaymentAction and PaymentActionSpecific, or it'll fail with 
                //PaymentAction : Required parameter missing (error code: 81115)
                PaymentAction = PaymentActionCodeType.Sale,
                PaymentActionSpecified = true
            };


            //prepare for commiting transaction
            var payReq = new DoExpressCheckoutPaymentReq {
                DoExpressCheckoutPaymentRequest = new DoExpressCheckoutPaymentRequestType {
                    Version = ApplicationHandler.Instance.PaypalVersion,
                    DoExpressCheckoutPaymentRequestDetails = new DoExpressCheckoutPaymentRequestDetailsType {
                        Token = resp.GetExpressCheckoutDetailsResponseDetails.Token,
                        //must specify PaymentAction and PaymentActionSpecific, or it'll fail with
                        //PaymentAction : Required parameter missing (error code: 81115)
                        PaymentAction = PaymentActionCodeType.Sale,
                        PaymentActionSpecified = true,
                        PayerID = resp.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID,
                        PaymentDetails = sPaymentDetails,
                    }
                }
            };

            //commit transaction and display results to user
            var doResponse =
                BuildPayPalWebservice().DoExpressCheckoutPayment(payReq);
            HandleError(doResponse);
            return doResponse;
        }

        #endregion

        #region "UtilMethods"
        //****************************
        //Util Methods
        //****************************
        public static PayPalAPIAASoapBinding BuildPayPalWebservice() {
            //more details on https://www.paypal.com/en_US/ebook/PP_APIReference/architecture.html
            var credentials = new UserIdPasswordType {
                Username = ApplicationHandler.Instance.PaypalAPIUsername,
                Password = ApplicationHandler.Instance.PaypalAPIPassword,
                Signature = ApplicationHandler.Instance.PaypalAPISignature,
            };

            var paypal = new PayPalAPIAASoapBinding();
            paypal.RequesterCredentials = new CustomSecurityHeaderType {
                Credentials = credentials
            };

            return paypal;
        }

        internal static void HandleError(AbstractResponseType resp) {
            if(resp.Errors != null && resp.Errors.Length > 0) {
                //errors occured - log them
                LoggerUtil.AuditLog(LogLevel.Error,
                                    AuditEventType.SUBMIT_PAYPAL_PAYMENT_FAILED,
                                    "StoreSite",
                                    null,
                                    null, null, null, null, null);
                throw new PayPalException(resp.Errors);
            }
        }

        #endregion
    }
}