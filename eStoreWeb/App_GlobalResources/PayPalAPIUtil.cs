using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.UI;
using com.domaintransformations.culture;
using com.domaintransformations.exceptions;
using eStoreWeb.com.paypal.sandbox.www;
using Slf;

namespace eStoreWeb {
    public class PayPalAPIUtil {
        static ILogger logger = LoggerService.GetLogger("ErrorLogger");

        #region "PaymentMethods"
        //****************************
        //Payment Methods
        //****************************
        public static void setupExpressCheckout(HttpRequest request, HttpResponse response) {
            //build request
            SetExpressCheckoutRequestDetailsType reqDetails = new SetExpressCheckoutRequestDetailsType();
            string basePath = request.Url.AbsoluteUri.Replace(request.Url.PathAndQuery, 
                                                              string.Empty) + 
                                                              request.ApplicationPath;

            reqDetails.ReturnURL = basePath + "Checkout4.aspx";
            reqDetails.CancelURL = basePath + "Checkout3.aspx";
            reqDetails.NoShipping = "1";
            reqDetails.OrderTotal = new BasicAmountType() {
                currencyID = CurrencyCodeType.AUD,
                Value = CultureService.toInternalLocalCulture(SessionHandler.TotalCost),
            };

            SetExpressCheckoutReq req = new SetExpressCheckoutReq() {
                SetExpressCheckoutRequest = new SetExpressCheckoutRequestType() {
                    Version = PayPalAPIUtil.Version,
                    SetExpressCheckoutRequestDetails = reqDetails
                }
            };

            //query PayPal and get token
            SetExpressCheckoutResponseType resp = BuildPayPalWebservice().SetExpressCheckout(req);
            HandleError(resp);

            //redirect user to PayPal
            response.Redirect(string.Format("{0}?cmd=_express-checkout&token={1}",
                                            ApplicationHandler.PaypalAPIURL, resp.Token));
        }

        public static void GetExpressCheckoutDetails(HttpRequest request, Page page) {
            if(!page.IsPostBack) {
                string token = request.QueryString["token"];

                //build getdetails request
                GetExpressCheckoutDetailsReq req = new GetExpressCheckoutDetailsReq() {
                    GetExpressCheckoutDetailsRequest = new GetExpressCheckoutDetailsRequestType() {
                        Version = PayPalAPIUtil.Version,
                        Token = token
                    }
                };

                //query PayPal for transaction details
                GetExpressCheckoutDetailsResponseType resp =
                    BuildPayPalWebservice().GetExpressCheckoutDetails(req);
                HandleError(resp);

                GetExpressCheckoutDetailsResponseDetailsType respDetails = resp.GetExpressCheckoutDetailsResponseDetails;

                SessionHandler.PPCheckoutData = resp;
                SessionHandler.PPCheckoutDetails = respDetails;
            }
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public static DoExpressCheckoutPaymentResponseType CompletePayPalTransaction() {
            //get transaction details
            GetExpressCheckoutDetailsResponseType resp = SessionHandler.PPCheckoutData;
            GetExpressCheckoutDetailsResponseDetailsType respDetails = SessionHandler.PPCheckoutDetails;

            //create a new object, because it is now an array !!!!
            PaymentDetailsType[] sPaymentDetails = new PaymentDetailsType[1];
            sPaymentDetails[0] = new PaymentDetailsType() {
                OrderTotal = new BasicAmountType() {
                    currencyID = respDetails.PaymentDetails[0].OrderTotal.currencyID,
                    Value = respDetails.PaymentDetails[0].OrderTotal.Value
                },
                //must specify PaymentAction and PaymentActionSpecific, or it'll fail with 
                //PaymentAction : Required parameter missing (error code: 81115)
                PaymentAction = PaymentActionCodeType.Sale,
                PaymentActionSpecified = true
            };


            //prepare for commiting transaction
            DoExpressCheckoutPaymentReq payReq = new DoExpressCheckoutPaymentReq() {
                DoExpressCheckoutPaymentRequest = new DoExpressCheckoutPaymentRequestType() {
                    Version = PayPalAPIUtil.Version,
                    DoExpressCheckoutPaymentRequestDetails = new DoExpressCheckoutPaymentRequestDetailsType() {
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
            DoExpressCheckoutPaymentResponseType doResponse =
                BuildPayPalWebservice().DoExpressCheckoutPayment(payReq);
            HandleError(doResponse);
            return doResponse;
        }

        #endregion

        #region "UtilMethods"
        //****************************
        //Util Methods
        //****************************       
        public static string Version {
            get { return ApplicationHandler.PaypalVersion; }
        }

        public static PayPalAPIAASoapBinding BuildPayPalWebservice() {
            //more details on https://www.paypal.com/en_US/ebook/PP_APIReference/architecture.html
            UserIdPasswordType credentials = new UserIdPasswordType() {
                Username = ApplicationHandler.PaypalAPIUsername,
                Password = ApplicationHandler.PaypalAPIPassword,
                Signature = ApplicationHandler.PaypalAPISignature,
            };

            PayPalAPIAASoapBinding paypal = new PayPalAPIAASoapBinding();
            paypal.RequesterCredentials = new CustomSecurityHeaderType() {
                Credentials = credentials
            };

            return paypal;
        }

        internal static void HandleError(AbstractResponseType resp) {
            if(resp.Errors != null && resp.Errors.Length > 0) {
                //errors occured - log them
                throw new PayPalException(resp.Errors);
            }
        }

        #endregion
    }
}