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
using System.Web.UI.WebControls;
using com.phoenixconsulting.culture;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.navigation;
using phoenixconsulting.paymentservice.paypal;

namespace eStoreWeb {
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
    public partial class Checkout3 : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            CameFrom.handleNavigationRedirect(Request.UrlReferrer);

            //string redirectPage = NavigationUtils.getInvalidNavigationRedirect(Request.UrlReferrer);
            //if(!redirectPage.Equals("")) {
            //    Response.Redirect(redirectPage);
            //}
            if(!Page.IsPostBack) {
                PopulateForm();
            }
            SetNoteLabel();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private void SetNoteLabel() {
            if(!(IsAustralia(SessionHandler.Instance.BillingCountry))) {
                NoteLabel.Text = "Note: All orders are processed in Australia dollars and are converted " +
                                 "into your local currency by your credit card provider. Your order for " +
                                 CultureService.getConvertedPrice(SessionHandler.Instance.TotalCost.ToString(CultureInfo.InvariantCulture), SessionHandler.Instance.BillingXRate, SessionHandler.Instance.CurrencyValue) +
                                 " will be processed as AUD " +
                                 CultureService.toLocalCulture(SessionHandler.Instance.TotalCost);
                NoteLabel.Visible = true;
            } else {
                NoteLabel.Visible = false;
            }
        }

        protected void GoToPreviousPage(object sender, EventArgs e) {
            GoTo.Instance.Checkout2Page();
        }

        protected void GoToNextCheckoutPage(object sender, EventArgs e) {
            //Page.Validate();
            //if (Page.IsValid)
            //{
            SetPaymentSessionValues();

            if(PaymentMethodRadioButtonList.SelectedValue.Equals("PayPal")) {
                PayPalApiUtil.SetupExpressCheckout(Request, Response);
            } else {
                GoTo.Instance.Checkout4Page();
            }
            //}
        }

        private void PopulateForm() {
            CardholderTextBox.Text = SessionHandler.Instance.CardholderName;
            cardTypeDDL.Text = SessionHandler.Instance.CardType;
            CardNumberTextBox.Text = SessionHandler.Instance.CardNumber;
            CardValidationValueTextBox.Text = SessionHandler.Instance.CVV;
            ExpiryMonthDDL.Text = SessionHandler.Instance.CCExpiryMonth;
            ExpiryYearDDL.Text = SessionHandler.Instance.CCExpiryYear;
        }

        private void SetPaymentSessionValues() {
            SessionHandler.Instance.CardholderName = CardholderTextBox.Text;
            SessionHandler.Instance.CardType = cardTypeDDL.Text;
            SessionHandler.Instance.CardNumber = CardNumberTextBox.Text;
            SessionHandler.Instance.CVV = CardValidationValueTextBox.Text;
            SessionHandler.Instance.CCExpiryMonth = ExpiryMonthDDL.Text;
            SessionHandler.Instance.CCExpiryYear = ExpiryYearDDL.Text;
            SessionHandler.Instance.CCExpiryMonthItem = ExpiryMonthDDL.SelectedItem.Text;
            SessionHandler.Instance.CCExpiryYearItem = ExpiryYearDDL.SelectedItem.Text;
            SessionHandler.Instance.PaymentType = GetPaymentTypeImage();
        }

        private string GetPaymentTypeImage() {
            if(PaymentMethodRadioButtonList.SelectedValue.Equals("PayPal")) {
                return PaymentMethodRadioButtonList.SelectedItem.Text;
            }
            switch(SessionHandler.Instance.CardType) {
                case "Visa":
                    return "<img src='/Images/VisaCard.gif'>";
                case "MasterCard":
                    return "<img src='/Images/MasterCard.gif'>";
                case "AMEX":
                    return "<img src='/Images/AmexCard.gif'>";
                default:
                    return "";
            }
        }

        protected void PaymentMethodRadioButtonList_SelectedIndexChanged(object sender, EventArgs e) {
            if(((RadioButtonList)sender).SelectedItem.Value == "PayPal") {
                DisableFieldsAndValidators();
            } else {
                EnableFieldsAndValidators();
            }
        }

        private void EnableFieldsAndValidators() {
            //FIELDS
            CardholderTextBox.Enabled = true;
            cardTypeDDL.Enabled = true;
            CardNumberTextBox.Enabled = true;
            CardValidationValueTextBox.Enabled = true;
            ExpiryMonthDDL.Enabled = true;
            ExpiryYearDDL.Enabled = true;

            //VALIDATORS
            CardholderTextBoxRequiredFieldValidator.Enabled = true;
            CardTypeDropDownListRequiredFieldValidator.Enabled = true;
            CardNumberTextBoxRequiredFieldValidator.Enabled = true;
            CardValidationValueTextBoxRequiredFieldValidator.Enabled = true;
            ExpiryMonthDDLRequiredFieldValidator.Enabled = true;
            ExpiryYearDDLRequiredFieldValidator.Enabled = true;
        }

        private void DisableFieldsAndValidators() {
            //FIELDS
            CardholderTextBox.Enabled = false;
            cardTypeDDL.Enabled = false;
            CardNumberTextBox.Enabled = false;
            CardValidationValueTextBox.Enabled = false;
            ExpiryMonthDDL.Enabled = false;
            ExpiryYearDDL.Enabled = false;

            //VALIDATORS
            CardholderTextBoxRequiredFieldValidator.Enabled = false;
            CardTypeDropDownListRequiredFieldValidator.Enabled = false;
            CardNumberTextBoxRequiredFieldValidator.Enabled = false;
            CardValidationValueTextBoxRequiredFieldValidator.Enabled = false;
            ExpiryMonthDDLRequiredFieldValidator.Enabled = false;
            ExpiryYearDDLRequiredFieldValidator.Enabled = false;
        }
    }
}