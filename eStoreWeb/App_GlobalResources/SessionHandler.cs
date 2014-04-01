using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Data;
using System.Web;
using eStoreWeb.com.paypal.sandbox.www;

namespace eStoreWeb {
    public static class SessionHandler {
        //Declare a string variable to hold the key name of the session variable
        //and use this string variable instead of typing the key name
        //in order to avoid spelling mistakes
        private static string _shoppingCart = "ShoppingCart";

        //General
        private static string _previousPage = "PreviousPage";
        private static string _currentPage = "CurrentPage";
        private static string _departmentID = "DepID";
        private static string _categoryID = "CatID";
        private static string _productID = "ProdID";
        private static string _browsePageIndex = "BrowsePageIndex";
        private static string _alsoBought = "AlsoBought";
        private static string _searchstring = "Searchstring";

        //ViewCart Page
        private static string _currencyXRate = "CurrencyXRate";
        private static string _currency = "Currency";
        private static string _currencyValue = "CurrencyValue";
        private static string _shippingMode = "ShipMode";
        private static string _totalCost = "TotalCost";
        private static string _totalWeight = "TotalWeight";
        private static string _totalShipping = "TotalShipping";

        //Checkout Page 1
        private static string _billingEmailAddress = "BillingDetailsEmailAddress";
        private static string _billingFirstName = "BillingDetailsFirstName";
        private static string _billingLastName = "BillingDetailsLastName";
        private static string _billingAddress = "BillingDetailsAddress";
        private static string _billingCitySuburb = "BillingDetailsCitySuburb";
        private static string _billingStateRegion = "BillingDetailsStateRegion";
        private static string _billingPostcode = "BillingDetailsPostCode";
        private static string _billingCountry = "BillingDetailsCountry";
        private static string _billingCountryCode = "BillingDetailsCountryCode";
        private static string _billingCurrencyId = "BillingCurrencyID";
        private static string _billingCurrencyValue = "BillingCountryCurrencyValue";
        private static string _billingXRate = "BillingCountryXRate";

        //Checkout Page 2
        private static string _sameAddress = "SameAddress";
        private static string _shippingFirstName = "ShippingDetailsFirstName";
        private static string _shippingLastName = "ShippingDetailsLastName";
        private static string _shippingAddress = "ShippingDetailsAddress";
        private static string _shippingCitySuburb = "ShippingDetailsCitySuburb";
        private static string _shippingStateRegion = "ShippingDetailsStateRegion";
        private static string _shippingPostcode = "ShippingDetailsPostCode";
        private static string _shippingCountry = "ShippingDetailsCountry";
        private static string _shippingCountryCode = "ShippingDetailsCountryCode";
        private static string _shippingCountryName = "ShippingDetailsCountryName";

        //Checkout Page 3
        private static string _paymentType = "PaymentType";
        private static string _cardHolderName = "CardHolderName";
        private static string _cardType = "CardType";
        private static string _cardNumber = "CardNumber";
        private static string _cvv = "CVV";
        private static string _ccExpiryMonth = "CCExpiryMonth";
        private static string _ccExpiryYear = "CCExpiryYear";
        private static string _ccExpiryMonthItemText = "CCExpiryMonthItemText";
        private static string _ccExpiryYearItemText = "CCExpiryYearItemText";

        //Checkout Page 4
        private static string _wrapping = "Wrapping";
        private static string _cartDataSet = "CartDataSet";
        private static string _comment = "Comment";
        private static string _giftTag = "GiftTag";

        //CCAuth
        private static string _preAuthID = "PreAuthID";
        private static string _txnID = "TxnID";

        //CC Processor Specific
        private static string _ccRequestXML = "RequestXML";
        private static string _ccResponseXML = "ResponseXML";
        private static string _ccURL = "URL";

        //Payment Result
        private static string _paymentResponseText = "PaymentResponseText";
        private static string _paymentResponseCode = "PaymentResponseCode";
        private static string _isPaymentApproved = "IsPaymentApproved";

        //PayPal Specific
        private static string _ppCheckOutData = "CheckoutData";
        private static string _ppCheckOutDetails = "CheckoutDetails";

        //Declare a static / shared strongly typed property to expose session variable
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public static ArrayList ShoppingCart {
            get {
                return getArrayList(SessionHandler._shoppingCart);
            }

            set {
                HttpContext.Current.Session[SessionHandler._shoppingCart] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string PreviousPage {
            get {
                return getString(SessionHandler._previousPage);
            }

            set {
                HttpContext.Current.Session[SessionHandler._previousPage] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CurrentPage {
            get {
                return getString(SessionHandler._currentPage);
            }

            set {
                HttpContext.Current.Session[SessionHandler._currentPage] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string DepartmentId {
            get {
                return getString(SessionHandler._departmentID);
            }

            set {
                HttpContext.Current.Session[SessionHandler._departmentID] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CategoryId {
            get {
                return getString(SessionHandler._categoryID);
            }

            set {
                HttpContext.Current.Session[SessionHandler._categoryID] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string ProductId {
            get {
                return getString(SessionHandler._productID);
            }

            set {
                HttpContext.Current.Session[SessionHandler._productID] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string BrowsePageIndex {
            get {
                return getString(SessionHandler._browsePageIndex);
            }

            set {
                HttpContext.Current.Session[SessionHandler._browsePageIndex] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static bool AlsoBought {
            get {
                return getBool(SessionHandler._alsoBought);
            }

            set {
                HttpContext.Current.Session[SessionHandler._alsoBought] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string SearchString {
            get {
                return getString(SessionHandler._searchstring);
            }

            set {
                HttpContext.Current.Session[SessionHandler._searchstring] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static double CurrencyXRate {
            get {
                return getDouble(SessionHandler._currencyXRate);
            }

            set {
                HttpContext.Current.Session[SessionHandler._currencyXRate] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string Currency {
            get {
                return getString(SessionHandler._currency);
            }

            set {
                HttpContext.Current.Session[SessionHandler._currency] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CurrencyValue {
            get {
                return getString(SessionHandler._currencyValue);
            }

            set {
                HttpContext.Current.Session[SessionHandler._currencyValue] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string ShippingMode {
            get {
                return getString(SessionHandler._shippingMode);
            }

            set {
                HttpContext.Current.Session[SessionHandler._shippingMode] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static double TotalCost {
            get {
                return getDouble(SessionHandler._totalCost);
            }

            set {
                HttpContext.Current.Session[SessionHandler._totalCost] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static double TotalWeight {
            get {
                return getDouble(SessionHandler._totalWeight);
            }

            set {
                HttpContext.Current.Session[SessionHandler._totalWeight] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static double TotalShipping {
            get {
                return getDouble(SessionHandler._totalShipping);
            }

            set {
                HttpContext.Current.Session[SessionHandler._totalShipping] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string BillingEmailAddress {
            get {
                return getString(SessionHandler._billingEmailAddress);
            }

            set {
                HttpContext.Current.Session[SessionHandler._billingEmailAddress] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string BillingFirstName {
            get {
                return getString(SessionHandler._billingFirstName);
            }

            set {
                HttpContext.Current.Session[SessionHandler._billingFirstName] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string BillingLastName {
            get {
                return getString(SessionHandler._billingLastName);
            }

            set {
                HttpContext.Current.Session[SessionHandler._billingLastName] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string BillingAddress {
            get {
                return getString(SessionHandler._billingAddress);
            }

            set {
                HttpContext.Current.Session[SessionHandler._billingAddress] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string BillingCitySuburb {
            get {
                return getString(SessionHandler._billingCitySuburb);
            }

            set {
                HttpContext.Current.Session[SessionHandler._billingCitySuburb] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string BillingStateRegion {
            get {
                return getString(SessionHandler._billingStateRegion);
            }

            set {
                HttpContext.Current.Session[SessionHandler._billingStateRegion] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string BillingPostcode {
            get {
                return getString(SessionHandler._billingPostcode);
            }

            set {
                HttpContext.Current.Session[SessionHandler._billingPostcode] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string BillingCountry {
            get {
                return getString(SessionHandler._billingCountry);
            }

            set {
                HttpContext.Current.Session[SessionHandler._billingCountry] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string BillingCountryCode {
            get {
                return getString(SessionHandler._billingCountryCode);
            }

            set {
                HttpContext.Current.Session[SessionHandler._billingCountryCode] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static string BillingCurrencyId {
            get {
                return getString(SessionHandler._billingCurrencyId);
            }

            set {
                HttpContext.Current.Session[SessionHandler._billingCurrencyId] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string BillingCurrencyValue {
            get {
                return getString(SessionHandler._billingCurrencyValue);
            }

            set {
                HttpContext.Current.Session[SessionHandler._billingCurrencyValue] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static double BillingXRate {
            get {
                return getDouble(SessionHandler._billingXRate);
            }

            set {
                HttpContext.Current.Session[SessionHandler._billingXRate] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string SameAddress {
            get {
                return getString(SessionHandler._sameAddress);
            }

            set {
                HttpContext.Current.Session[SessionHandler._sameAddress] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string ShippingFirstName {
            get {
                return getString(SessionHandler._shippingFirstName);
            }

            set {
                HttpContext.Current.Session[SessionHandler._shippingFirstName] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string ShippingLastName {
            get {
                return getString(SessionHandler._shippingLastName);
            }

            set {
                HttpContext.Current.Session[SessionHandler._shippingLastName] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string ShippingAddress {
            get {
                return getString(SessionHandler._shippingAddress);
            }

            set {
                HttpContext.Current.Session[SessionHandler._shippingAddress] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string ShippingCitySuburb {
            get {
                return getString(SessionHandler._shippingCitySuburb);
            }

            set {
                HttpContext.Current.Session[SessionHandler._shippingCitySuburb] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string ShippingStateRegion {
            get {
                return getString(SessionHandler._shippingStateRegion);
            }

            set {
                HttpContext.Current.Session[SessionHandler._shippingStateRegion] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string ShippingPostcode {
            get {
                return getString(SessionHandler._shippingPostcode);
            }

            set {
                HttpContext.Current.Session[SessionHandler._shippingPostcode] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string ShippingCountry {
            get {
                return getString(SessionHandler._shippingCountry);
            }

            set {
                HttpContext.Current.Session[SessionHandler._shippingCountry] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string ShippingCountryCode {
            get {
                return getString(SessionHandler._shippingCountryCode);
            }

            set {
                HttpContext.Current.Session[SessionHandler._shippingCountryCode] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string ShippingCountryName {
            get {
                return getString(SessionHandler._shippingCountryName);
            }

            set {
                HttpContext.Current.Session[SessionHandler._shippingCountryName] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string PaymentType {
            get {
                return getString(SessionHandler._paymentType);
            }

            set {
                HttpContext.Current.Session[SessionHandler._paymentType] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CardholderName {
            get {
                return getString(SessionHandler._cardHolderName);
            }

            set {
                HttpContext.Current.Session[SessionHandler._cardHolderName] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CardType {
            get {
                return getString(SessionHandler._cardType);
            }

            set {
                HttpContext.Current.Session[SessionHandler._cardType] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CardNumber {
            get {
                return getString(SessionHandler._cardNumber);
            }

            set {
                HttpContext.Current.Session[SessionHandler._cardNumber] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public static string CVV {
            get {
                return getString(SessionHandler._cvv);
            }

            set {
                HttpContext.Current.Session[SessionHandler._cvv] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCExpiryMonth {
            get {
                return getString(SessionHandler._ccExpiryMonth);
            }

            set {
                HttpContext.Current.Session[SessionHandler._ccExpiryMonth] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCExpiryYear {
            get {
                return getString(SessionHandler._ccExpiryYear);
            }

            set {
                HttpContext.Current.Session[SessionHandler._ccExpiryYear] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCExpiryMonthItem {
            get {
                return getString(SessionHandler._ccExpiryMonthItemText);
            }

            set {
                HttpContext.Current.Session[SessionHandler._ccExpiryMonthItemText] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCExpiryYearItem {
            get {
                return getString(SessionHandler._ccExpiryYearItemText);
            }

            set {
                HttpContext.Current.Session[SessionHandler._ccExpiryYearItemText] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static bool Wrapping {
            get {
                return getBool(SessionHandler._wrapping);
            }

            set {
                HttpContext.Current.Session[SessionHandler._wrapping] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string PreAuthId {
            get {
                return getString(SessionHandler._preAuthID);
            }

            set {
                HttpContext.Current.Session[SessionHandler._preAuthID] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string TxnId {
            get {
                return getString(SessionHandler._txnID);
            }

            set {
                HttpContext.Current.Session[SessionHandler._txnID] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string Comment {
            get {
                return getString(SessionHandler._comment);
            }

            set {
                HttpContext.Current.Session[SessionHandler._comment] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string GiftTag {
            get {
                return getString(SessionHandler._giftTag);
            }

            set {
                HttpContext.Current.Session[SessionHandler._giftTag] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static DataSet CartDataSet {
            get {
                return getDataSet(SessionHandler._cartDataSet);
            }

            set {
                HttpContext.Current.Session[SessionHandler._cartDataSet] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string PaymentResponseText {
            get {
                return getString(SessionHandler._paymentResponseText);
            }

            set {
                HttpContext.Current.Session[SessionHandler._paymentResponseText] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string PaymentResponseCode {
            get {
                return getString(SessionHandler._paymentResponseCode);
            }

            set {
                HttpContext.Current.Session[SessionHandler._paymentResponseCode] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string IsPaymentApproved {
            get {
                return getString(SessionHandler._isPaymentApproved);
            }

            set {
                HttpContext.Current.Session[SessionHandler._isPaymentApproved] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCRequestXml {
            get {
                return getString(SessionHandler._ccRequestXML);
            }

            set {
                HttpContext.Current.Session[SessionHandler._ccRequestXML] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string CCResponseXml {
            get {
                return getString(SessionHandler._ccResponseXML);
            }

            set {
                HttpContext.Current.Session[SessionHandler._ccResponseXML] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static string Url {
            get {
                return getString(SessionHandler._ccURL);
            }

            set {
                HttpContext.Current.Session[SessionHandler._ccURL] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static GetExpressCheckoutDetailsResponseType PPCheckoutData {
            get {
                return getECDResponseType(SessionHandler._ppCheckOutData);
            }

            set {
                HttpContext.Current.Session[SessionHandler._ppCheckOutData] = value;
            }
        }

        //Declare a static / shared strongly typed property to expose session variable
        public static GetExpressCheckoutDetailsResponseDetailsType PPCheckoutDetails {
            get {
                return getECDResponseDetailsType(SessionHandler._ppCheckOutDetails);
            }

            set {
                HttpContext.Current.Session[SessionHandler._ppCheckOutDetails] = value;
            }
        }

        //*********************************
        // HELPER METHODS
        //*********************************
        private static int getInt(string sessionVariable) {
            object check = HttpContext.Current.Session[sessionVariable];
            //Check for null first
            if(check == null) {
                return 0;
            } else {
                return (int)check;
            }
        }

        private static string getString(string sessionVariable) {
            object check = HttpContext.Current.Session[sessionVariable];
            //Check for null first
            if(check == null) {
                return string.Empty;
            } else {
                return check.ToString();
            }
        }

        private static double getDouble(string sessionVariable) {
            object check = HttpContext.Current.Session[sessionVariable];
            //Check for null first
            if(check == null) {
                return 0;
            } else {
                return (double)check;
            }
        }

        private static bool getBool(string sessionVariable) {
            object check = HttpContext.Current.Session[sessionVariable];
            //Check for null first
            if(check == null) {
                return false;
            } else {
                return (bool)check;
            }
        }

        private static ArrayList getArrayList(string sessionVariable) {
            if(HttpContext.Current.Session[sessionVariable] == null) {
                return null;
            } else {
                return (ArrayList)HttpContext.Current.Session[sessionVariable];
            }
        }

        private static DataSet getDataSet(string sessionVariable) {
            if(HttpContext.Current.Session[sessionVariable] == null) {
                return null;
            } else {
                return (DataSet)HttpContext.Current.Session[sessionVariable];
            }
        }

        private static GetExpressCheckoutDetailsResponseType getECDResponseType(string sessionVariable) {
            if(HttpContext.Current.Session[sessionVariable] == null) {
                return null;
            } else {
                return (GetExpressCheckoutDetailsResponseType)HttpContext.Current.Session[sessionVariable];
            }
        }

        private static GetExpressCheckoutDetailsResponseDetailsType getECDResponseDetailsType(string sessionVariable) {
            if(HttpContext.Current.Session[sessionVariable] == null) {
                return null;
            } else {
                return (GetExpressCheckoutDetailsResponseDetailsType)HttpContext.Current.Session[sessionVariable];
            }
        }
    }
}