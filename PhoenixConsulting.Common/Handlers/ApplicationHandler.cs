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
using phoenixconsulting.common.basepages;

namespace phoenixconsulting.common.handlers {
    public sealed class ApplicationHandler : SingletonBase<ApplicationHandler> {
        private ApplicationHandler() {
        }

        #region Application Variable Name Constants

        //Declare a string variable to hold the key name of the session variable
        //and use this string variable instead of typing the key name
        //in order to avoid spelling mistakes
        private static string _tradingName = "TradingName";

        //General
        private const string _CCProcessorMerchantID = "MerchantID";
        private static string _CCProcessorPassword = "Password";
        private static string _CCProcessorPaymentType = "PaymentType";
        private static string _CCProcessorLiveEchoURL = "LiveEchoURL";
        private static string _CCProcessorTestEchoURL = "TestEchoURL";
        private static string _CCProcessorLivePaymentURL = "LivePaymentURL";
        private static string _CCProcessorTestPaymentURL = "TestPaymentURL";
        private static string _CCProcessorLivePaymentDCURL = "LivePaymentDCURL";
        private static string _CCProcessorTestPaymentDCURL = "TestPaymentDCURL";
        private static string _CCProcessorRequestType = "CCRequestType";
        private static string _CCProcessorServer = "CCServer";
        private static string _refundAdminFeePercent = "RefundAdminFeePercent";
        private static string _cancelAdminFeePercent = "CancelAdminFeePercent";
        private static string _systemEmailAddress = "SystemEmailAddress";
        private static string _smtpServer = "SMTPServer";
        private static string _homePageURL = "HomePageURL";
        private static string _supportEmail = "SupportEmail";
        private static string _paypalVersion = "PaypalVersion";
        private static string _paypalAPIUsername = "PaypalAPIUsername";
        private static string _paypalAPIPassword = "PaypalAPIPassword";
        private static string _paypalAPISignature = "PaypalAPISignature";
        private static string _paypalAPIURL = "PaypalAPIURL";
        private static string _paypalURL = "PaypalURL";

        #endregion

        #region Properties And Methods
        public string TradingName {
            get { return getString(_tradingName); }
            set { setString(_tradingName, value); }
        }

        public string CCProcessorMerchantID {
            get { return getString(_CCProcessorMerchantID); }
            set { setString(_CCProcessorMerchantID, value); }
        }

        public string CCProcessorPassword {
            get { return getString(_CCProcessorPassword); }
            set { setString(_CCProcessorPassword, value); }
        }

        public int CCProcessorPaymentType {
            get { return getInt(_CCProcessorPaymentType); }
            set { setInt(_CCProcessorPaymentType, value); }
        }

        public string CCProcessorLiveEchoURL {
            get { return getString(_CCProcessorLiveEchoURL); }
            set { setString(_CCProcessorLiveEchoURL, value); }
        }

        public string CCProcessorTestEchoURL {
            get { return getString(_CCProcessorTestEchoURL); }
            set { setString(_CCProcessorTestEchoURL, value); }
        }

        public string CCProcessorLivePaymentURL {
            get { return getString(_CCProcessorLivePaymentURL); }
            set { setString(_CCProcessorLivePaymentURL, value); }
        }

        public string CCProcessorTestPaymentURL {
            get { return getString(_CCProcessorTestPaymentURL); }
            set { setString(_CCProcessorTestPaymentURL, value); }
        }

        public string CCProcessorLivePaymentDCURL {
            get { return getString(_CCProcessorLivePaymentDCURL); }
            set { setString(_CCProcessorLivePaymentDCURL, value); }
        }

        public string CCProcessorTestPaymentDCURL {
            get { return getString(_CCProcessorTestPaymentDCURL); }
            set { setString(_CCProcessorTestPaymentDCURL, value); }
        }

        public string CCProcessorRequestType {
            get { return getString(_CCProcessorRequestType); }
            set { setString(_CCProcessorRequestType, value); }
        }

        public string CCProcessorServer {
            get { return getString(_CCProcessorServer); }
            set { setString(_CCProcessorServer, value); }
        }

        public double RefundAdminFeePercent {
            get { return getDouble(_refundAdminFeePercent); }
            set { setDouble(_refundAdminFeePercent, value); }
        }

        public double CancelAdminFeePercent {
            get { return getDouble(_cancelAdminFeePercent); }
            set { setDouble(_cancelAdminFeePercent, value); }
        }

        public string SystemEmailAddress {
            get { return getString(_systemEmailAddress); }
            set { setString(_systemEmailAddress, value); }
        }

        public string SMTPServer {
            get { return getString(_smtpServer); }
            set { setString(_smtpServer, value); }
        }

        public string HomePageURL {
            get { return getString(_homePageURL); }
            set { setString(_homePageURL, value); }
        }

        public string SupportEmail {
            get { return getString(_supportEmail); }
            set { setString(_supportEmail, value); }
        }

        public string PaypalVersion {
            get { return getString(_paypalVersion); }
            set { setString(_paypalVersion, value); }
        }

        public string PaypalAPIUsername {
            get { return getString(_paypalAPIUsername); }
            set { setString(_paypalAPIUsername, value); }
        }

        public string PaypalAPIPassword {
            get { return getString(_paypalAPIPassword); }
            set { setString(_paypalAPIPassword, value); }
        }

        public string PaypalAPISignature {
            get { return getString(_paypalAPISignature); }
            set { setString(_paypalAPISignature, value); }
        }

        public string PaypalAPIURL {
            get { return getString(_paypalAPIURL); }
            set { setString(_paypalAPIURL, value); }
        }

        public string PaypalURL {
            get { return getString(_paypalURL); }
            set { setString(_paypalURL, value); }
        }

        #endregion

        #region Convenience Methods
        private void setInt(string variableName, int val) {
            setInt(variableName, HTTPScope.APPLICATION, val);
        }

        private void setString(string variableName, string val) {
            setString(variableName, HTTPScope.APPLICATION, val);
        }

        private void setDouble(string variableName, double val) {
            setDouble(variableName, HTTPScope.APPLICATION, val);
        }

        private void setBool(string variableName, bool val) {
            setBool(variableName, HTTPScope.APPLICATION, val);
        }

        private int getInt(string variableName) {
            return getInt(variableName, HTTPScope.APPLICATION);
        }

        private string getString(string variableName) {
            return getString(variableName, HTTPScope.APPLICATION);
        }

        private double getDouble(string variableName) {
            return getDouble(variableName, HTTPScope.APPLICATION);
        }

        private bool getBool(string variableName) {
            return getBool(variableName, HTTPScope.APPLICATION);
        }
        #endregion
    }
}