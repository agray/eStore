using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests {
    [TestFixture]
    public class Checkout3Tests {
        private ISelenium selenium;
        private StringBuilder verificationErrors;
        private TestUtilities testUtil;

        [SetUp]
        public void SetupTest() {
            selenium = new DefaultSelenium("localhost", 4444, "*firefox", "http://www.mywipstore.com.au/");
            //selenium = new DefaultSelenium("localhost", 4444, "*firefox", "http://www.google.com/");
            selenium.Start();
            verificationErrors = new StringBuilder();
            testUtil = new TestUtilities();
        }

        [TearDown]
        public void TeardownTest() {
            try {
                selenium.Stop();
                testUtil = null;
            } catch(Exception) {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void LoadCheckout3Test() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);

            testUtil.EnterCheckOut1DataAndSubmit(selenium);
            testUtil.EnterCheckOut2DataAndSubmit(selenium);

            string htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("3. Enter your payment details"));
            Assert.IsTrue(htmlSource.Contains("value=\"CC\" checked=\"checked\" type=\"radio\">"));
        }

        [Test]
        public void Checkout3RequiredFieldTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);
            testUtil.EnterCheckOut1DataAndSubmit(selenium);
            testUtil.EnterCheckOut2DataAndSubmit(selenium);
            testUtil.WaitForPageToLoad(selenium, "6000");
            selenium.Click("ctl00_ContentPlaceHolder1_NextButton");
            testUtil.WaitForPageToLoad(selenium, "10000");

            testUtil.AssertCheckout3RequiredFieldRedLabels(selenium);
        }

        [Test]
        public void PayPalDisablesCCFieldsTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);
            testUtil.EnterCheckOut1DataAndSubmit(selenium);
            testUtil.EnterCheckOut2DataAndSubmit(selenium);

            selenium.Click("ctl00_ContentPlaceHolder1_PaymentMethodRadioButtonList_0");
            testUtil.WaitForPageToLoad(selenium, "2000");

            Assert.IsTrue(testUtil.IsDisabled(selenium, "ctl00_ContentPlaceHolder1_CardholderTextBox"));
            Assert.IsTrue(testUtil.IsDisabled(selenium, "ctl00_ContentPlaceHolder1_cardTypeDDL_CardTypeDropDownList"));
            Assert.IsTrue(testUtil.IsDisabled(selenium, "ctl00_ContentPlaceHolder1_CardNumberTextBox"));
            Assert.IsTrue(testUtil.IsDisabled(selenium, "ctl00_ContentPlaceHolder1_CardValidationValueTextBox"));
            Assert.IsTrue(testUtil.IsDisabled(selenium, "ctl00_ContentPlaceHolder1_ExpiryMonthDDL_ExpiryMonthDropDownList"));
            Assert.IsTrue(testUtil.IsDisabled(selenium, "ctl00_ContentPlaceHolder1_ExpiryYearDDL_ExpiryYearDropDownList"));
        }

        [Test]
        public void PayPalDisablesCCFieldsAndRetainsValuesTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);
            testUtil.EnterCheckOut1DataAndSubmit(selenium);
            testUtil.EnterCheckOut2DataAndSubmit(selenium);
            testUtil.EnterCheckOut3Data(selenium);

            selenium.Click("ctl00_ContentPlaceHolder1_PaymentMethodRadioButtonList_0");
            testUtil.WaitForPageToLoad(selenium, "3000");

            Assert.IsTrue(testUtil.IsDisabled(selenium, "ctl00_ContentPlaceHolder1_CardholderTextBox"));
            Assert.IsTrue(testUtil.IsDisabled(selenium, "ctl00_ContentPlaceHolder1_cardTypeDDL_CardTypeDropDownList"));
            Assert.IsTrue(testUtil.IsDisabled(selenium, "ctl00_ContentPlaceHolder1_CardNumberTextBox"));
            Assert.IsTrue(testUtil.IsDisabled(selenium, "ctl00_ContentPlaceHolder1_CardValidationValueTextBox"));
            Assert.IsTrue(testUtil.IsDisabled(selenium, "ctl00_ContentPlaceHolder1_ExpiryMonthDDL_ExpiryMonthDropDownList"));
            Assert.IsTrue(testUtil.IsDisabled(selenium, "ctl00_ContentPlaceHolder1_ExpiryYearDDL_ExpiryYearDropDownList"));

            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_CardholderTextBox", "Andre Valleta"));
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_cardTypeDDL_CardTypeDropDownList", "Visa"));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_CardNumberTextBox", "12344"));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_CardValidationValueTextBox", "1234"));
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_ExpiryMonthDDL_ExpiryMonthDropDownList", "02"));
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_ExpiryYearDDL_ExpiryYearDropDownList", "2011"));
        }

        [Test]
        public void CCRadioEnablesCCFieldsAndRetainsValuesTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);
            testUtil.EnterCheckOut1DataAndSubmit(selenium);
            testUtil.EnterCheckOut2DataAndSubmit(selenium);
            testUtil.EnterCheckOut3Data(selenium);

            selenium.Click("ctl00_ContentPlaceHolder1_PaymentMethodRadioButtonList_0");
            testUtil.WaitForPageToLoad(selenium, "3000");

            selenium.Click("ctl00_ContentPlaceHolder1_PaymentMethodRadioButtonList_1");
            testUtil.WaitForPageToLoad(selenium, "3000");

            Assert.IsTrue(testUtil.IsEnabled(selenium, "ctl00_ContentPlaceHolder1_CardholderTextBox"));
            Assert.IsTrue(testUtil.IsEnabled(selenium, "ctl00_ContentPlaceHolder1_cardTypeDDL_CardTypeDropDownList"));
            Assert.IsTrue(testUtil.IsEnabled(selenium, "ctl00_ContentPlaceHolder1_CardNumberTextBox"));
            Assert.IsTrue(testUtil.IsEnabled(selenium, "ctl00_ContentPlaceHolder1_CardValidationValueTextBox"));
            Assert.IsTrue(testUtil.IsEnabled(selenium, "ctl00_ContentPlaceHolder1_ExpiryMonthDDL_ExpiryMonthDropDownList"));
            Assert.IsTrue(testUtil.IsEnabled(selenium, "ctl00_ContentPlaceHolder1_ExpiryYearDDL_ExpiryYearDropDownList"));

            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_CardholderTextBox", "Andre Valleta"));
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_cardTypeDDL_CardTypeDropDownList", "Visa"));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_CardNumberTextBox", "12344"));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_CardValidationValueTextBox", "1234"));
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_ExpiryMonthDDL_ExpiryMonthDropDownList", "02"));
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_ExpiryYearDDL_ExpiryYearDropDownList", "2011"));
        }


        [Test]
        public void CVVHelpPageTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);
            testUtil.EnterCheckOut1DataAndSubmit(selenium);
            testUtil.EnterCheckOut2DataAndSubmit(selenium);

            selenium.Click("ctl00_ContentPlaceHolder1_CVVHyperlink");

            testUtil.MoveToPopUp(selenium, "2000");

            string htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(selenium.GetTitle().Equals("What is CVV"));
            Assert.IsTrue(htmlSource.Contains("<h1>Card Verification Value (CVV)</h1>"));
            Assert.IsTrue(htmlSource.Contains("<h2>Visa Card and Master Card</h2>"));
            Assert.IsTrue(htmlSource.Contains("For Visa and MasterCard the CVV number is the last 3 digits on the Signature Panel"));
            Assert.IsTrue(htmlSource.Contains("on the back of the credit card."));
            Assert.IsTrue(htmlSource.Contains("<img id=\"CVVMasterVisaImage\" src=\"Images/CVVMasterVisa.jpg\" alt=\"CVV Master Visa\" style=\"border-width: 0px;\">"));
            Assert.IsTrue(htmlSource.Contains("<h2>Amex</h2>"));
            Assert.IsTrue(htmlSource.Contains("For American Express, the CVV number is the last 4 digits (printed in white) on"));
            Assert.IsTrue(htmlSource.Contains("the front of the credit card."));
            Assert.IsTrue(htmlSource.Contains("<img id=\"Image1\" src=\"Images/CVVAmex.jpg\" alt=\"CVV Amex\" style=\"border-width: 0px;\">"));
        }
    }
}