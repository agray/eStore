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
using eStoreBLL;
using eStoreDAL;
using eStoreWeb.Controls;
using eStoreWeb.Properties;
using phoenixconsulting.businessentities.account;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.navigation;

namespace eStoreWeb {
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
    public partial class checkout1 : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            //NavigationUtils.handleCheckOut1NavigationRedirect();
            
            //string redirectPage = NavigationUtils.getCheckOut1InvalidNavigationRedirect();
            //if(!redirectPage.Equals(String.Empty)) {
            //    Response.Redirect(redirectPage);
            //}

            //if(!Page.IsPostBack) {
            //    if((!String.IsNullOrEmpty(SessionHandler.BillingEmailAddress))) {
            //        populateForm();
            //        countryDDL.SelectedValue = SessionHandler.BillingCountry;
            //    } else {
            //        countryDDL.SelectedValue = SessionHandler.ShippingCountry;
            //    }
            //}

            if(!Page.IsPostBack) {
                populateForm();
            }
        }

        private void populateForm() {
            if(isLoggedIn()) {
                if(SessionHandler.Instance.IsBillingAddressSet()) {
                    SetFormToBillingSessionDetails();
                } else {
                    //See if customer has address stored
                    Guid userID = BaseUserControl.getUserID(this);
                    CustomerAddressBLL ca = new CustomerAddressBLL();
                    DTAddress address = ca.getCustomerBillingAddress(userID);
                    if(address != null) {
                        CustomerFirstNameTextBox.Text = address.FirstName;
                        CustomerLastNameTextBox.Text = address.LastName;
                        CustomerAddressTextBox.Text = address.StreetAddress;
                        CustomerSuburbTextBox.Text = address.SuburbCity;
                        CustomerStateTextBox.Text = address.StateProvinceRegion;
                        CustomerPostcodeTextBox.Text = address.ZipPostCode;
                    }
                }
            } else {
                if(SessionHandler.Instance.IsBillingAddressSet()) {
                    SetFormToBillingSessionDetails();
                } else {
                    countryDDL.SelectedValue = SessionHandler.Instance.ShippingCountry;
                }
            }
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected void countryDDL_SelectedIndexChanged(object sender, EventArgs e) {
            SessionHandler.Instance.BillingCountry = countryDDL.SelectedValue;
            //Set currency
            SetCurrencyByCountry(Int32.Parse(SessionHandler.Instance.BillingCountry));
        }

        protected void GoToViewCartPage(object sender, EventArgs e) {
            GoTo.Instance.ViewCartPage();
        }
        
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected void GoToNextCheckoutPage(object sender, EventArgs e) {
            //if(CaptchaCV.IsValid) {
            SetBillingSessionDetails();
            GetBillingCurrencyDetails();

            //this is for ViewCart Page
            if(IsSameAddress()) {
                SessionHandler.Instance.ShippingCountry = countryDDL.SelectedValue;
            }
            if(IsAustralia(SessionHandler.Instance.ShippingCountry)) {
                SessionHandler.Instance.ShippingMode = Settings.Default.ModeExpressAirMail;
            }

            if(CurrentAddressCheckBox.Checked) {
                CustomerAddressBLL ca = new CustomerAddressBLL();
                ca.saveBillingAddress(BaseUserControl.getUserID(this),
                                      CustomerFirstNameTextBox.Text,
                                      CustomerLastNameTextBox.Text,
                                      CustomerAddressTextBox.Text,
                                      CustomerSuburbTextBox.Text,
                                      CustomerStateTextBox.Text,
                                      CustomerPostcodeTextBox.Text,
                                      int.Parse(countryDDL.SelectedValue));
            }

            GoTo.Instance.Checkout2Page();
            //}
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private static void GetBillingCurrencyDetails() {
            CurrenciesBLL currTableAdapter = new CurrenciesBLL();
            DAL.CurrencyRow currRow = default(DAL.CurrencyRow);

            currRow = currTableAdapter.getCurrencyByBillingCountry(Int32.Parse(SessionHandler.Instance.BillingCountry))[0];

            SessionHandler.Instance.BillingCurrencyValue = currRow.Value;
            SessionHandler.Instance.BillingCurrencyId = currRow.ID.ToString();
            SessionHandler.Instance.BillingXRate = currRow.ExchangeRate;
        }

        private void SetBillingSessionDetails() {
            SessionHandler.Instance.BillingEmailAddress = EmailTextBox.Text;
            SessionHandler.Instance.BillingFirstName = CustomerFirstNameTextBox.Text;
            SessionHandler.Instance.BillingLastName = CustomerLastNameTextBox.Text;
            SessionHandler.Instance.BillingAddress = CustomerAddressTextBox.Text;
            SessionHandler.Instance.BillingCitySuburb = CustomerSuburbTextBox.Text;
            SessionHandler.Instance.BillingStateRegion = CustomerStateTextBox.Text;
            SessionHandler.Instance.BillingPostcode = CustomerPostcodeTextBox.Text;
            SessionHandler.Instance.BillingCountry = countryDDL.SelectedValue;
            SessionHandler.Instance.BillingCountryCode = new CountriesBLL().getCountryCode(Int32.Parse(SessionHandler.Instance.BillingCountry));
        }

        private void SetFormToBillingSessionDetails() {
            EmailTextBox.Text = SessionHandler.Instance.BillingEmailAddress;
            CustomerFirstNameTextBox.Text = SessionHandler.Instance.BillingFirstName;
            CustomerLastNameTextBox.Text = SessionHandler.Instance.BillingLastName;
            CustomerAddressTextBox.Text = SessionHandler.Instance.BillingAddress;
            CustomerSuburbTextBox.Text = SessionHandler.Instance.BillingCitySuburb;
            CustomerStateTextBox.Text = SessionHandler.Instance.BillingStateRegion;
            CustomerPostcodeTextBox.Text = SessionHandler.Instance.BillingPostcode;
            countryDDL.SelectedValue = SessionHandler.Instance.BillingCountry;
        }

        //protected void CaptchaTextBox_CustomServerValidate(object sender, ServerValidateEventArgs e) {
        //    CaptchaControl1.ValidateCaptcha(CaptchaTextBox.Text);
        //    e.IsValid = CaptchaControl1.UserValidated;
        //}
    }
}