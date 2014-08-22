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
using System.Collections;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using phoenixconsulting.common.basepages;
using PhoenixConsulting.PaymentService.PayPal.com.paypal.sandbox.www;

namespace phoenixconsulting.common.handlers {
    public sealed class SessionHandler : SingletonBase<SessionHandler> {
        private SessionHandler() {}

        #region Application Variable Name Constants

        //Declare a string variable to hold the key name of the session variable
        //and use this string variable instead of typing the key name
        //in order to avoid spelling mistakes
        private const string _shoppingCart = "ShoppingCart";
        private const string _wishList = "WishList";

        //General
        private const string _previousPage = "PreviousPage";
        private const string _currentPage = "CurrentPage";
        private const string _departmentID = "DepID";
        private const string _categoryID = "CatID";
        private const string _productID = "ProdID";
        private const string _browsePageIndex = "BrowsePageIndex";
        private const string _alsoBought = "AlsoBought";
        private const string _searchstring = "Searchstring";

        //ViewCart Page
        private const string _currencyXRate = "CurrencyXRate";
        private const string _currency = "Currency";
        private const string _currencyValue = "CurrencyValue";
        private const string _shippingMode = "ShipMode";
        private const string _totalCost = "TotalCost";
        private const string _totalWeight = "TotalWeight";
        private const string _totalShipping = "TotalShipping";

        //Checkout Page 1
        private const string _billingEmailAddress = "BillingDetailsEmailAddress";
        private const string _billingFirstName = "BillingDetailsFirstName";
        private const string _billingLastName = "BillingDetailsLastName";
        private const string _billingAddress = "BillingDetailsAddress";
        private const string _billingCitySuburb = "BillingDetailsCitySuburb";
        private const string _billingStateRegion = "BillingDetailsStateRegion";
        private const string _billingPostcode = "BillingDetailsPostCode";
        private const string _billingCountry = "BillingDetailsCountry";
        private const string _billingCountryCode = "BillingDetailsCountryCode";
        private const string _billingCurrencyId = "BillingCurrencyID";
        private const string _billingCurrencyValue = "BillingCountryCurrencyValue";
        private const string _billingXRate = "BillingCountryXRate";

        //Checkout Page 2
        private const string _sameAddress = "SameAddress";
        private const string _shippingFirstName = "ShippingDetailsFirstName";
        private const string _shippingLastName = "ShippingDetailsLastName";
        private const string _shippingAddress = "ShippingDetailsAddress";
        private const string _shippingCitySuburb = "ShippingDetailsCitySuburb";
        private const string _shippingStateRegion = "ShippingDetailsStateRegion";
        private const string _shippingPostcode = "ShippingDetailsPostCode";
        private const string _shippingCountry = "ShippingDetailsCountry";
        private const string _shippingCountryCode = "ShippingDetailsCountryCode";
        private const string _shippingCountryName = "ShippingDetailsCountryName";

        //Checkout Page 3
        private const string _paymentType = "PaymentType";
        private const string _cardHolderName = "CardHolderName";
        private const string _cardType = "CardType";
        private const string _cardNumber = "CardNumber";
        private const string _cvv = "CVV";
        private const string _ccExpiryMonth = "CCExpiryMonth";
        private const string _ccExpiryYear = "CCExpiryYear";
        private const string _ccExpiryMonthItemText = "CCExpiryMonthItemText";
        private const string _ccExpiryYearItemText = "CCExpiryYearItemText";

        //Checkout Page 4
        private const string _wrapping = "Wrapping";
        private const string _cartDataSet = "CartDataSet";
        private const string _comment = "Comment";
        private const string _giftTag = "GiftTag";

        //CCAuth
        private const string _preAuthID = "PreAuthID";
        private const string _txnID = "TxnID";

        //CC Processor Specific
        private const string _ccRequestXML = "RequestXML";
        private const string _ccResponseXML = "ResponseXML";
        private const string _ccURL = "URL";

        //Payment Result
        private const string _paymentResponseText = "PaymentResponseText";
        private const string _paymentResponseCode = "PaymentResponseCode";
        private const string _isPaymentApproved = "IsPaymentApproved";

        //PayPal Specific
        private const string _ppCheckOutData = "CheckoutData";
        private const string _ppCheckOutDetails = "CheckoutDetails";

        //Customer Login Fields
        private const string _loginFirstName = "LoginFirstName";
        private const string _loginLastName = "LoginLastName";
        private const string _loginEmail = "LoginEmail";
        private const string _loginRequireNewsletter = "LoginRequireNewsletter";

        #endregion

        #region Properties And Methods

        public void SetCurrency(string code, double XRate, string id) {
            Instance.CurrencyValue = code;
            Instance.CurrencyXRate = XRate;
            Instance.Currency = id;
        }

        public ArrayList ShoppingCart {
            get { return getArrayList(_shoppingCart); }
            set { setArrayList(_shoppingCart, value); }
        }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ArrayList WishList {
            get { return getArrayList(_wishList); }
            set { setArrayList(_wishList, value); }
        }

        public string PreviousPage {
            get { return getString(_previousPage); }
            set { setString(_previousPage, value); }
        }

        public string CurrentPage {
            get { return getString(_currentPage); }
            set { setString(_currentPage, value); }
        }

        public int DepartmentId {
            get { return getInt(_departmentID); }
            set { setInt(_departmentID, value); }
        }

        public int CategoryId {
            get { return getInt(_categoryID); }
            set { setInt(_categoryID, value); }
        }

        public int ProductId {
            get { return getInt(_productID); }
            set { setInt(_productID, value); }
        }

        public string BrowsePageIndex {
            get { return getString(_browsePageIndex); }
            set { setString(_browsePageIndex, value); }
        }

        public bool AlsoBought {
            get { return getBool(_alsoBought); }
            set { setBool(_alsoBought, value); }
        }

        public string SearchString {
            get { return getString(_searchstring); }
            set { setString(_searchstring, value); }
        }

        public double CurrencyXRate {
            get { return getDouble(_currencyXRate); }
            set { setDouble(_currencyXRate, value); }
        }

        public string Currency {
            get { return getString(_currency); }
            set { setString(_currency, value); }
        }

        public string CurrencyValue {
            get { return getString(_currencyValue); }
            set { setString(_currencyValue, value); }
        }

        public string ShippingMode {
            get { return getString(_shippingMode); }
            set { setString(_shippingMode, value); }
        }

        public double TotalCost {
            get { return getDouble(_totalCost); }
            set { setDouble(_totalCost, value); }
        }

        public double TotalWeight {
            get { return getDouble(_totalWeight); }
            set { setDouble(_totalWeight, value); }
        }

        public double TotalShipping {
            get { return getDouble(_totalShipping); }
            set { setDouble(_totalShipping, value); }
        }

        public string BillingEmailAddress {
            get { return getString(_billingEmailAddress); }
            set { setString(_billingEmailAddress, value); }
        }

        public string BillingFirstName {
            get { return getString(_billingFirstName); }
            set { setString(_billingFirstName, value); }
        }

        public string BillingLastName {
            get { return getString(_billingLastName); }
            set { setString(_billingLastName, value); }
        }

        public string BillingAddress {
            get { return getString(_billingAddress); }
            set { setString(_billingAddress, value); }
        }

        public string BillingCitySuburb {
            get { return getString(_billingCitySuburb); }
            set { setString(_billingCitySuburb, value); }
        }

        public string BillingStateRegion {
            get { return getString(_billingStateRegion); }
            set { setString(_billingStateRegion, value); }
        }

        public string BillingPostcode {
            get { return getString(_billingPostcode); }
            set { setString(_billingPostcode, value); }
        }

        public string BillingCountry {
            get { return getString(_billingCountry); }
            set { setString(_billingCountry, value); }
        }

        public string BillingCountryCode {
            get { return getString(_billingCountryCode); }
            set { setString(_billingCountryCode, value); }
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string BillingCurrencyId {
            get { return getString(_billingCurrencyId); }
            set { setString(_billingCurrencyId, value); }
        }

        public string BillingCurrencyValue {
            get { return getString(_billingCurrencyValue); }
            set { setString(_billingCurrencyValue, value); }
        }

        public double BillingXRate {
            get { return getDouble(_billingXRate); }
            set { setDouble(_billingXRate, value); }
        }

        public string SameAddress {
            get { return getString(_sameAddress); }
            set { setString(_sameAddress, value); }
        }

        public string ShippingFirstName {
            get { return getString(_shippingFirstName); }
            set { setString(_shippingFirstName, value); }
        }

        public string ShippingLastName {
            get { return getString(_shippingLastName); }
            set { setString(_shippingLastName, value); }
        }

        public string ShippingAddress {
            get { return getString(_shippingAddress); }
            set { setString(_shippingAddress, value); }
        }

        public string ShippingCitySuburb {
            get { return getString(_shippingCitySuburb); }
            set { setString(_shippingCitySuburb, value); }
        }

        public string ShippingStateRegion {
            get { return getString(_shippingStateRegion); }
            set { setString(_shippingStateRegion, value); }
        }

        public string ShippingPostcode {
            get { return getString(_shippingPostcode); }
            set { setString(_shippingPostcode, value); }
        }

        public string ShippingCountry {
            get { return getString(_shippingCountry); }
            set { setString(_shippingCountry, value); }
        }

        public string ShippingCountryCode {
            get { return getString(_shippingCountryCode); }
            set { setString(_shippingCountryCode, value); }
        }

        public string ShippingCountryName {
            get { return getString(_shippingCountryName); }
            set { setString(_shippingCountryName, value); }
        }

        public string PaymentType {
            get { return getString(_paymentType); }
            set { setString(_paymentType, value); }
        }

        public string CardholderName {
            get { return getString(_cardHolderName); }
            set { setString(_cardHolderName, value); }
        }

        public string CardType {
            get { return getString(_cardType); }
            set { setString(_cardType, value); }
        }

        public string CardNumber {
            get { return getString(_cardNumber); }
            set { setString(_cardNumber, value); }
        }

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string CVV {
            get { return getString(_cvv); }
            set { setString(_cvv, value); }
        }

        public string CCExpiryMonth {
            get { return getString(_ccExpiryMonth); }
            set { setString(_ccExpiryMonth, value); }
        }

        public string CCExpiryYear {
            get { return getString(_ccExpiryYear); }
            set { setString(_ccExpiryYear, value); }
        }

        public string CCExpiryMonthItem {
            get { return getString(_ccExpiryMonthItemText); }
            set { setString(_ccExpiryMonthItemText, value); }
        }

        public string CCExpiryYearItem {
            get { return getString(_ccExpiryYearItemText); }
            set { setString(_ccExpiryYearItemText, value); }
        }

        public bool Wrapping {
            get { return getBool(_wrapping); }
            set { setBool(_wrapping, value); }
        }

        public string PreAuthId {
            get { return getString(_preAuthID); }
            set { setString(_preAuthID, value); }
        }

        public string TxnId {
            get { return getString(_txnID); }
            set { setString(_txnID, value); }
        }

        public string Comment {
            get { return getString(_comment); }
            set { setString(_comment, value); }
        }

        public string GiftTag {
            get { return getString(_giftTag); }
            set { setString(_giftTag, value); }
        }

        public DataSet CartDataSet {
            get { return getDataSet(_cartDataSet); }
            set { setDataSet(_cartDataSet, value); }
        }

        public string PaymentResponseText {
            get { return getString(_paymentResponseText); }
            set { setString(_paymentResponseText, value); }
        }

        public string PaymentResponseCode {
            get { return getString(_paymentResponseCode); }
            set { setString(_paymentResponseCode, value); }
        }

        public string IsPaymentApproved {
            get { return getString(_isPaymentApproved); }
            set { setString(_isPaymentApproved, value); }
        }

        public string CCRequestXml {
            get { return getString(_ccRequestXML); }
            set { setString(_ccRequestXML, value); }
        }

        public string CCResponseXml {
            get { return getString(_ccResponseXML); }
            set { setString(_ccResponseXML, value); }
        }

        public string Url {
            get { return getString(_ccURL); }
            set { setString(_ccURL, value); }
        }

        public GetExpressCheckoutDetailsResponseType PPCheckoutData {
            get { return getECDResponseType(_ppCheckOutData); }
            set { setECDResponseType(_ppCheckOutData, value); }
        }

        public GetExpressCheckoutDetailsResponseDetailsType PPCheckoutDetails {
            get { return getECDResponseDetailsType(_ppCheckOutDetails); }
            set { setECDResponseDetailsType(_ppCheckOutDetails, value); }
        }

        public string LoginFirstName {
            get { return getString(_loginFirstName); }
            set { setString(_loginFirstName, value); }
        }

        public string LoginLastName {
            get { return getString(_loginLastName); }
            set { setString(_loginLastName, value); }
        }

        public string LoginEmailAddress {
            get { return getString(_loginEmail); }
            set { setString(_loginEmail, value); }
        }

        public bool LoginRequireNewsletter {
            get { return getBool(_loginRequireNewsletter); }
            set { setBool(_loginRequireNewsletter, value); }
        }

        #endregion

        #region Convenience Methods

        private void setInt(string variableName, int val) {
            SetInt(variableName, HTTPScope.SESSION, val);
        }

        private void setString(string variableName, string val) {
            SetString(variableName, HTTPScope.SESSION, val);
        }

        private void setDouble(string variableName, double val) {
            SetDouble(variableName, HTTPScope.SESSION, val);
        }

        private void setBool(string variableName, bool val) {
            SetBool(variableName, HTTPScope.SESSION, val);
        }

        private void setArrayList(string variableName, ArrayList list) {
            SetArrayList(variableName, HTTPScope.SESSION, list);
        }

        private void setDataSet(string variableName, DataSet set) {
            SetDataSet(variableName, HTTPScope.SESSION, set);
        }

        private void setECDResponseType(string variableName, GetExpressCheckoutDetailsResponseType type) {
            SetEcdResponseType(variableName, HTTPScope.SESSION, type);
        }

        private void setECDResponseDetailsType(string variableName, GetExpressCheckoutDetailsResponseDetailsType type) {
            SetEcdResponseDetailsType(variableName, HTTPScope.SESSION, type);
        }

        private int getInt(string variableName) {
            return GetInt(variableName, HTTPScope.SESSION);
        }

        private string getString(string variableName) {
            return GetString(variableName, HTTPScope.SESSION);
        }

        private double getDouble(string variableName) {
            return GetDouble(variableName, HTTPScope.SESSION);
        }

        private bool getBool(string variableName) {
            return GetBool(variableName, HTTPScope.SESSION);
        }

        private ArrayList getArrayList(string variableName) {
            return GetArrayList(variableName, HTTPScope.SESSION);
        }

        private DataSet getDataSet(string variableName) {
            return GetDataSet(variableName, HTTPScope.SESSION);
        }

        public bool IsBillingAddressSet() {
            return BillingEmailAddress != string.Empty && BillingFirstName != string.Empty && BillingLastName != string.Empty && BillingAddress != string.Empty && BillingCitySuburb != string.Empty && BillingStateRegion != string.Empty && BillingPostcode != string.Empty && BillingCountry != string.Empty;
        }

        public bool IsShippingAddressSet() {
            return ShippingFirstName != string.Empty && ShippingLastName != string.Empty && ShippingAddress != string.Empty && ShippingCitySuburb != string.Empty && ShippingStateRegion != string.Empty && ShippingPostcode != string.Empty && ShippingCountry != string.Empty;
        }

        private GetExpressCheckoutDetailsResponseType getECDResponseType(string variableName) {
            return GetEcdResponseType(variableName, HTTPScope.SESSION);
        }

        private GetExpressCheckoutDetailsResponseDetailsType getECDResponseDetailsType(string variableName) {
            return GetEcdResponseDetailsType(variableName, HTTPScope.SESSION);
        }

        #endregion

    }
}