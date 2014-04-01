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
using System.Web.UI;
using eStoreBLL;
using eStoreWeb.Controls;
using phoenixconsulting.businessentities.account;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.enums.account;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.navigation;

namespace eStoreWeb {
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
    public partial class checkout2 : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            CameFrom.handleNavigationRedirect(Request.UrlReferrer);

            //string redirectPage = NavigationUtils.getInvalidNavigationRedirect(Request.UrlReferrer);
            //if(!redirectPage.Equals(String.Empty)) {
            //    Response.Redirect(redirectPage);
            //}

            //if(!Page.IsPostBack) {
            //    if(CameFrom.Checkout3Page()) {
            //        sameAddressDDL.SelectedValue = SessionHandler.SameAddress;
            //    }
            //    if(DTUtil.IsSameAddress()) {
            //        SetFormValuesToBillingDetails();
            //    } else {
            //        SetFormValuesToShippingDetails();
            //    }
            //}
            if(!Page.IsPostBack) {
                populateForm();
            }
        }

        private void populateForm() {
            if(CameFrom.Checkout3Page()) {
                sameAddressDDL.SelectedValue = SessionHandler.Instance.SameAddress;
            }

            if(isLoggedIn()) {
                if(SessionHandler.Instance.IsShippingAddressSet()) {
                    if(IsSameAddress()) {
                        SetFormValuesToBillingDetails();
                    } else {
                        SetFormValuesToShippingDetails();
                    }
                } else {
                    //See if customer has address stored
                    Guid userID = BaseUserControl.getUserID(this);
                    CustomerAddressBLL ca = new CustomerAddressBLL();
                    DTAddress address = ca.getCustomerShippingAddress(userID);
                    if(address != null) {
                        ShippingFirstNameTextBox.Text = address.FirstName;
                        ShippingLastNameTextBox.Text = address.LastName;
                        ShippingAddressTextBox.Text = address.StreetAddress;
                        ShippingSuburbTextBox.Text = address.SuburbCity;
                        ShippingStateTextBox.Text = address.StateProvinceRegion;
                        ShippingPostcodeTextBox.Text = address.ZipPostCode;
                        setSameAddressDDL(address);
                    }
                }
            } else {
                if(IsSameAddress()) {
                    SetFormValuesToBillingDetails();
                } else {
                    SetFormValuesToShippingDetails();
                }
            }
        }

        private void setSameAddressDDL(DTAddress shippingAddress) {
            DTAddress billingAddress = new DTAddress(0, AddressType.BILLING,
                                                     SessionHandler.Instance.BillingFirstName,
                                                     SessionHandler.Instance.BillingLastName,
                                                     SessionHandler.Instance.BillingAddress,
                                                     SessionHandler.Instance.BillingCitySuburb,
                                                     SessionHandler.Instance.BillingStateRegion,
                                                     SessionHandler.Instance.BillingPostcode,
                                                     int.Parse(SessionHandler.Instance.BillingCountry));
            if(shippingAddress.Equals(billingAddress)){
                sameAddressDDL.SelectedValue = "1";
                SetValidatorStatus(ShippingFieldsPanel, false); //Disable Fields
            } else {
                sameAddressDDL.SelectedValue = "0";
                SetValidatorStatus(ShippingFieldsPanel, true); //Enable Fields
            }
        }

        protected void SameAddressDDL_SelectedIndexChanged(object sender, EventArgs e) {
            SessionHandler.Instance.SameAddress = sameAddressDDL.SelectedValue;
            if(IsSameAddress()) {
                SetFormValuesToBillingDetails();
            } else {
                ResetForm();
            }
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected void countryDDL_SelectedIndexChanged(object sender, EventArgs e) {
            SessionHandler.Instance.ShippingCountry = countryDDL.SelectedValue;
            SessionHandler.Instance.ShippingCountryName = countryDDL.SelectedItem.Text;
        }

        protected void GoToPreviousPage(object sender, EventArgs e) {
            GoTo.Instance.Checkout1Page();
        }

        protected void GoToNextCheckoutPage(object sender, EventArgs e) {
            Page.Validate();
            if(Page.IsValid) {
                SetShippingSessionValues();

                if(CurrentAddressCheckBox.Checked) {
                    CustomerAddressBLL ca = new CustomerAddressBLL();
                    ca.saveShippingAddress(BaseUserControl.getUserID(this), 
                                           ShippingFirstNameTextBox.Text,
                                           ShippingLastNameTextBox.Text,
                                           ShippingAddressTextBox.Text,
                                           ShippingSuburbTextBox.Text,
                                           ShippingStateTextBox.Text,
                                           ShippingPostcodeTextBox.Text,
                                           int.Parse(countryDDL.SelectedValue));
                }

                GoTo.Instance.Checkout3Page();
            }
        }
   
        private void ResetForm() {
            EmptyTextBoxValues(ShippingFieldsPanel);
            SetValidatorStatus(ShippingFieldsPanel, true); //Enable Fields
            countryDDL.SelectedValue = String.IsNullOrEmpty(SessionHandler.Instance.ShippingCountry)
                                           ? "7"
                                           : SessionHandler.Instance.ShippingCountry;
        }

        private void SetFormValuesToBillingDetails() {
            SetValidatorStatus(ShippingFieldsPanel, false); //Disable Fields
            ShippingFirstNameTextBox.Text = SessionHandler.Instance.BillingFirstName;
            ShippingLastNameTextBox.Text = SessionHandler.Instance.BillingLastName;
            ShippingAddressTextBox.Text = SessionHandler.Instance.BillingAddress;
            ShippingSuburbTextBox.Text = SessionHandler.Instance.BillingCitySuburb;
            ShippingStateTextBox.Text = SessionHandler.Instance.BillingStateRegion;
            ShippingPostcodeTextBox.Text = SessionHandler.Instance.BillingPostcode;
            countryDDL.DataBind();
            countryDDL.SelectedValue = SessionHandler.Instance.BillingCountry;
        }

        private void SetFormValuesToShippingDetails() {
            SetValidatorStatus(ShippingFieldsPanel, true); //Enable Fields
            sameAddressDDL.SelectedValue = SessionHandler.Instance.SameAddress;
            ShippingFirstNameTextBox.Text = SessionHandler.Instance.ShippingFirstName;
            ShippingLastNameTextBox.Text = SessionHandler.Instance.ShippingLastName;
            ShippingAddressTextBox.Text = SessionHandler.Instance.ShippingAddress;
            ShippingSuburbTextBox.Text = SessionHandler.Instance.ShippingCitySuburb;
            ShippingStateTextBox.Text = SessionHandler.Instance.ShippingStateRegion;
            ShippingPostcodeTextBox.Text = SessionHandler.Instance.ShippingPostcode;
            countryDDL.SelectedValue = SessionHandler.Instance.ShippingCountry;
        }

        private void SetShippingSessionValues() {
            SessionHandler.Instance.SameAddress = sameAddressDDL.SelectedValue;
            SessionHandler.Instance.ShippingFirstName = ShippingFirstNameTextBox.Text;
            SessionHandler.Instance.ShippingLastName = ShippingLastNameTextBox.Text;
            SessionHandler.Instance.ShippingAddress = ShippingAddressTextBox.Text;
            SessionHandler.Instance.ShippingCitySuburb = ShippingSuburbTextBox.Text;
            SessionHandler.Instance.ShippingStateRegion = ShippingStateTextBox.Text;
            SessionHandler.Instance.ShippingPostcode = ShippingPostcodeTextBox.Text;
            //countryDDL.DataBind();
            if(IsSameAddress()) {
                SessionHandler.Instance.ShippingCountry = SessionHandler.Instance.BillingCountry;
                SessionHandler.Instance.ShippingCountryName = (string)new CountriesBLL().getCountryByID(int.Parse(SessionHandler.Instance.BillingCountry)).Rows[0]["Name"];
                SessionHandler.Instance.ShippingCountryCode = SessionHandler.Instance.BillingCountryCode;
            } else {
                SessionHandler.Instance.ShippingCountry = countryDDL.SelectedValue;
                SessionHandler.Instance.ShippingCountryName = countryDDL.SelectedItem.Text;
                SessionHandler.Instance.ShippingCountryCode = new CountriesBLL().getCountryCode(Int32.Parse(SessionHandler.Instance.ShippingCountry));
            }
        }
    }
}