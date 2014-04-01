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
using eStoreBLL;
using eStoreWeb.Controls;
using phoenixconsulting.businessentities.account;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.enums.account;
using phoenixconsulting.common.handlers;

namespace eStoreWeb.Profile {
    public partial class AccountAddress : BasePage {

        protected void Page_PreRender(object sender, EventArgs e) {
            if(!Page.IsPostBack) {
                billingCountryDDL.DataBind();
                shippingCountryDDL.DataBind();
                Guid userID = BaseUserControl.getUserID(this);

                CustomerAddressBLL ca = new CustomerAddressBLL();
                DTAddress[] addresses = ca.getCustomerAddresses(userID);

                setFields(addresses);

                SessionHandler.Instance.SameAddress = sameAddressDDL.SelectedValue;
                AddressCountLabel.Text = addresses.Length.ToString();
            }
        }

        private void setFields(DTAddress[] addresses) {
            switch(addresses.Length) {
                case 1:
                    populateOneAddress(addresses[0]);
                    if(addresses[0].AddressType == AddressType.BILLING) {
                        populateShippingAddress(addresses[0]);
                    }
                    break;
                case 2:
                    populateTwoAddresses(addresses);
                    break;
            }
            setSameAddressDDL(addresses);
        }

        private void setSameAddressDDL(DTAddress[] addresses) {
            switch(addresses.Length) {
                case 0:
                    sameAddressDDL.SelectedValue = "1"; //Yes
                    SetValidatorStatus(ShippingFieldsPanel, false); //Disable Fields
                    break;
                case 1:
                    if(addresses[0].AddressType == AddressType.BILLING) {
                        sameAddressDDL.SelectedValue = "1"; //Yes
                        SetValidatorStatus(ShippingFieldsPanel, false); //Disable Fields
                    } else {
                        sameAddressDDL.SelectedValue = "0"; //No
                        SetValidatorStatus(ShippingFieldsPanel, true); //Enable Fields
                    }
                    break;
                case 2:
                    if(addresses[0].Equals(addresses[1])) {
                        sameAddressDDL.SelectedValue = "1"; //Yes
                        SetValidatorStatus(ShippingFieldsPanel, false); //Disable Fields
                    } else {
                        sameAddressDDL.SelectedValue = "0"; //No
                        SetValidatorStatus(ShippingFieldsPanel, true); //Enable Fields
                    }
                    break;
            }
        }

        private void populateTwoAddresses(DTAddress[] addresses) {
            populateOneAddress(addresses[0]);
            populateOneAddress(addresses[1]);
        }

        private void populateOneAddress(DTAddress address) {
            if(address.AddressType == AddressType.BILLING) {
                populateBillingAddress(address);
                //SetShippingFieldsToBillingDetails();
            } else {
                populateShippingAddress(address);
            }
        }

        private void populateBillingAddress(DTAddress address) {
            BillingAddressIDLabel.Text = address.Id.ToString();
            CustomerFirstNameTextBox.Text = address.FirstName.ToString();
            CustomerLastNameTextBox.Text = address.LastName.ToString();
            CustomerAddressTextBox.Text = address.StreetAddress.ToString();
            CustomerSuburbTextBox.Text = address.SuburbCity.ToString();
            CustomerStateTextBox.Text = address.StateProvinceRegion.ToString();
            CustomerPostcodeTextBox.Text = address.ZipPostCode.ToString();
            billingCountryDDL.SelectedValue = address.CountryId.ToString();
        }

        private void populateShippingAddress(DTAddress address) {
            ShippingAddressIDLabel.Text = address.Id.ToString();
            ShippingFirstNameTextBox.Text = address.FirstName.ToString();
            ShippingLastNameTextBox.Text = address.LastName.ToString();
            ShippingAddressTextBox.Text = address.StreetAddress.ToString();
            ShippingSuburbTextBox.Text = address.SuburbCity.ToString();
            ShippingStateTextBox.Text = address.StateProvinceRegion.ToString();
            ShippingPostcodeTextBox.Text = address.ZipPostCode.ToString();
            shippingCountryDDL.SelectedValue = address.CountryId.ToString();
        }

        protected void SameAddressDDL_SelectedIndexChanged(object sender, EventArgs e) {
            SessionHandler.Instance.SameAddress = sameAddressDDL.SelectedValue;
            if(IsSameAddress()) {
                SetShippingFieldsToBillingDetails();
            } else {
                ResetForm();
            }
        }

        private void SetShippingFieldsToBillingDetails() {
            SetValidatorStatus(ShippingFieldsPanel, false); //Disable Fields
            ShippingFirstNameTextBox.Text = CustomerFirstNameTextBox.Text;
            ShippingLastNameTextBox.Text = CustomerLastNameTextBox.Text;
            ShippingAddressTextBox.Text = CustomerAddressTextBox.Text;
            ShippingSuburbTextBox.Text = CustomerSuburbTextBox.Text;
            ShippingStateTextBox.Text = CustomerStateTextBox.Text;
            ShippingPostcodeTextBox.Text = CustomerPostcodeTextBox.Text;
            shippingCountryDDL.SelectedValue = billingCountryDDL.SelectedValue;
        }

        private void ResetForm() {
            EmptyTextBoxValues(ShippingFieldsPanel);
            SetValidatorStatus(ShippingFieldsPanel, true); //Enable Fields
            shippingCountryDDL.SelectedValue = String.IsNullOrEmpty(SessionHandler.Instance.ShippingCountry)
                                                   ? "7"
                                                   : SessionHandler.Instance.ShippingCountry;
        }

        protected void billingCountryDDL_SelectedIndexChanged(object sender, EventArgs e) {
            if(IsSameAddress()) {
                shippingCountryDDL.SelectedValue = billingCountryDDL.SelectedValue;
            }
        }

        protected void CustomerFirstNameTextBox_TextChanged(object sender, EventArgs e) {
            if(IsSameAddress()) {
                ShippingFirstNameTextBox.Text = CustomerFirstNameTextBox.Text;
            }
        }

        protected void CustomerLastNameTextBox_TextChanged(object sender, EventArgs e) {
            if(IsSameAddress()) {
                ShippingLastNameTextBox.Text = CustomerLastNameTextBox.Text;
            }
        }

        protected void CustomerAddressTextBox_TextChanged(object sender, EventArgs e) {
            if(IsSameAddress()) {
                ShippingAddressTextBox.Text = CustomerAddressTextBox.Text;
            }
        }

        protected void CustomerSuburbTextBox_TextChanged(object sender, EventArgs e) {
            if(IsSameAddress()) {
                ShippingSuburbTextBox.Text = CustomerSuburbTextBox.Text;
            }
        }

        protected void CustomerStateTextBox_TextChanged(object sender, EventArgs e) {
            if(IsSameAddress()) {
                ShippingStateTextBox.Text = CustomerStateTextBox.Text;
            }
        }

        protected void CustomerPostcodeTextBox_TextChanged(object sender, EventArgs e) {
            if(IsSameAddress()) {
                ShippingPostcodeTextBox.Text = CustomerPostcodeTextBox.Text;
            }
        }

        protected void SaveChanges(object sender, EventArgs e) {
            Guid userID = BaseUserControl.getUserID(this);
            CustomerAddressBLL ca = new CustomerAddressBLL();
            ca.saveAddresses(userID,
                             int.Parse(AddressCountLabel.Text),
                             new DTAddress(IntParseOrZero(BillingAddressIDLabel.Text.ToString()),
                                           AddressType.BILLING,
                                           CustomerFirstNameTextBox.Text,
                                           CustomerLastNameTextBox.Text,
                                           CustomerAddressTextBox.Text,
                                           CustomerSuburbTextBox.Text,
                                           CustomerStateTextBox.Text,
                                           CustomerPostcodeTextBox.Text,
                                           int.Parse(billingCountryDDL.SelectedValue)),
                             new DTAddress(IntParseOrZero(ShippingAddressIDLabel.Text.ToString()),
                                           AddressType.SHIPPING,
                                           ShippingFirstNameTextBox.Text,
                                           ShippingLastNameTextBox.Text,
                                           ShippingAddressTextBox.Text,
                                           ShippingSuburbTextBox.Text,
                                           ShippingStateTextBox.Text,
                                           ShippingPostcodeTextBox.Text,
                                           int.Parse(shippingCountryDDL.SelectedValue)));
        }
    }
}