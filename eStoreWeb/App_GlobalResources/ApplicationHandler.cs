using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Data;
using System.Web;

namespace eStoreWeb {
    public static class ApplicationHandler {
        //Declare a string variable to hold the key name of the session variable
        //and use this string variable instead of typing the key name
        //in order to avoid spelling mistakes
        private static string _tradingName = "TradingName";

        //General
        private static string _CCProcessorMerchantID = "MerchantID";
        private static string _CCProcessorPassword = "Password";
        private static string _CCProcessorPaymentType = "PaymentType";
        private static string _CCProcessorLiveEchoURL = "LiveEchoURL";
        private static string _CCProcessorTestEchoURL = "TestEchoURL";
        private static string _CCProcessorLivePaymentURL = "LivePaymentURL";
        private static string _CCProcessorTestPaymentURL = "TestPaymentURL";
        private static string _CCProcessorLivePaymentDCURL = "LivePaymentDCURL";
        private static string _CCProcessorTestPaymentDCURL = "TestPaymentDCURL";
        private static string _refundAdminFeePercent = "RefundAdminFeePercent";
        private static string _cancelAdminFeePercent = "CancelAdminFeePercent";
        private static string _systemEmailAddress = "SystemEmailAddress";
        private static string _smtpServer = "SMTPServer";
        private static string _homePageURL = "HomePageURL";
        private static string _supportEmail = "SupportEmail";
        private static string _paypalVersion = "PaypalVersion";
        private static string _paypalUsername = "PaypalUsername";
        private static string _paypalAPIUsername = "PaypalAPIUsername";
        private static string _paypalAPIPassword = "PaypalAPIPassword";
        private static string _paypalAPISignature = "PaypalAPISignature";
        private static string _paypalAPIURL = "PaypalAPIURL";
        private static string _paypalURL = "PaypalURL";
        
        //Declare a static / shared strongly typed property to expose session variable
        public static string TradingName {
            get {
                return getString(ApplicationHandler._tradingName);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._tradingName] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCProcessorMerchantID {
            get {
                return getString(ApplicationHandler._CCProcessorMerchantID);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._CCProcessorMerchantID] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCProcessorPassword {
            get {
                return getString(ApplicationHandler._CCProcessorPassword);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._CCProcessorPassword] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static int CCProcessorPaymentType {
            get {
                return getInt(ApplicationHandler._CCProcessorPaymentType);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._CCProcessorPaymentType] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCProcessorLiveEchoURL {
            get {
                return getString(ApplicationHandler._CCProcessorLiveEchoURL);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._CCProcessorLiveEchoURL] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCProcessorTestEchoURL {
            get {
                return getString(ApplicationHandler._CCProcessorTestEchoURL);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._CCProcessorTestEchoURL] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCProcessorLivePaymentURL {
            get {
                return getString(ApplicationHandler._CCProcessorLivePaymentURL);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._CCProcessorLivePaymentURL] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCProcessorTestPaymentURL {
            get {
                return getString(ApplicationHandler._CCProcessorTestPaymentURL);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._CCProcessorTestPaymentURL] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCProcessorLivePaymentDCURL {
            get {
                return getString(ApplicationHandler._CCProcessorLivePaymentDCURL);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._CCProcessorLivePaymentDCURL] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCProcessorTestPaymentDCURL {
            get {
                return getString(ApplicationHandler._CCProcessorTestPaymentDCURL);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._CCProcessorTestPaymentDCURL] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static double RefundAdminFeePercent {
            get {
                return getDouble(ApplicationHandler._refundAdminFeePercent);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._refundAdminFeePercent] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static double CancelAdminFeePercent {
            get {
                return getDouble(ApplicationHandler._cancelAdminFeePercent);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._cancelAdminFeePercent] = value;
            }
        }

        public static string SystemEmailAddress {
            get {
                return getString(ApplicationHandler._systemEmailAddress);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._systemEmailAddress] = value;
            }
        }


        //Declare a static / shared strongly typed property to expose session variable
        public static string SMTPServer {
            get {
                return getString(ApplicationHandler._smtpServer);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._smtpServer] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string HomePageURL {
            get {
                return getString(ApplicationHandler._homePageURL);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._homePageURL] = value;
            }
        }

        public static string SupportEmail {
            get {
                return getString(ApplicationHandler._supportEmail);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._supportEmail] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string PaypalVersion {
            get {
                return getString(ApplicationHandler._paypalVersion);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._paypalVersion] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string PaypalUsername {
            get {
                return getString(ApplicationHandler._paypalUsername);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._paypalUsername] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string PaypalAPIUsername {
            get {
                return getString(ApplicationHandler._paypalAPIUsername);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._paypalAPIUsername] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string PaypalAPIPassword {
            get {
                return getString(ApplicationHandler._paypalAPIPassword);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._paypalAPIPassword] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string PaypalAPISignature {
            get {
                return getString(ApplicationHandler._paypalAPISignature);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._paypalAPISignature] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string PaypalAPIURL {
            get {
                return getString(ApplicationHandler._paypalAPIURL);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._paypalAPIURL] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string PaypalURL {
            get {
                return getString(ApplicationHandler._paypalURL);
            }

            set {
                HttpContext.Current.Application[ApplicationHandler._paypalURL] = value;
            }
        }
        
        //*********************************
        // HELPER METHODS
        //*********************************
        private static int getInt(string applicationVariable) {
            object check = HttpContext.Current.Application[applicationVariable];
            //Check for null first
            if(check == null) {
                return 0;
            } else {
                return (int)check;
            }
        }

        private static string getString(string applicationVariable) {
            object check = HttpContext.Current.Application[applicationVariable];

            //Check for null first
            if(check == null) {
                return string.Empty;
            } else {
                return check.ToString();
            }
        }

        private static double getDouble(string applicationVariable) {
            object check = HttpContext.Current.Application[applicationVariable];
            //Check for null first
            if(check == null) {
                return 0;
            } else {
                return (double)check;
            }
        }
    }
}