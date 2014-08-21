using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests {
    [TestFixture]
    public class Checkout1Tests {
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
        public void EnterCheckoutTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);

            Assert.IsTrue(selenium.GetHtmlSource().Contains("1. Enter your billing details"));
        }

        [Test]
        public void Checkout1RequiredFieldTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);
            selenium.Click("ctl00_ContentPlaceHolder1_NextButton");

            Assert.IsTrue(selenium.GetHtmlSource().Contains("<span id=\"ctl00_ContentPlaceHolder1_EmailTextBoxRFV\" style=\"color: Red; display: inline;\">Required Field</span>"));
            testUtil.AssertCheckout1RequiredFieldRedLabels(selenium, "inline");
        }

        [Test]
        public void Checkout1InvalidEmailTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);

            selenium.Type("ctl00_ContentPlaceHolder1_EmailTextBox", "vb@com");
            selenium.Click("ctl00_ContentPlaceHolder1_NextButton");
            testUtil.WaitForPageToLoad(selenium, "6000");

            var htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_EmailTextBoxRFV\" style=\"color: Red; display: none;\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_EmailTextBoxREV\" style=\"color: Red; display: inline;\">Invalid Email Address</span>"));
            testUtil.AssertCheckout1RequiredFieldRedLabels(selenium, "inline");
        }

        [Test]
        public void EnsureBillingCountrySetFromViewCartTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.ChangeShippingCountryHelper(selenium, "Finland", "//option[@value='11']");
            testUtil.EnterCheckOutPages(selenium);

            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_countryDDL_CountryDropDownList", "Finland"));
        }

        [Test]
        public void EnsureShippingCountryUnchangedOnVCWhenBackOnlyTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.ChangeShippingCountryHelper(selenium, "Finland", "//option[@value='11']");
            testUtil.EnterCheckOutPages(selenium);

            selenium.Select("ctl00_ContentPlaceHolder1_countryDDL_CountryDropDownList", "label=Belgium");
            testUtil.WaitForPageToLoad(selenium, "6000");
            selenium.Click("ctl00_ContentPlaceHolder1_BackButton");
            testUtil.WaitForPageToLoad(selenium, "6000");

            //On ViewCart page at this point
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry", "Finland"));
        }

        [Test]
        public void EnsureShippingCountryChangedWhenNextTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.ChangeShippingCountryHelper(selenium, "Finland", "//option[@value='11']");
            testUtil.EnterCheckOutPages(selenium);

            selenium.Select("ctl00_ContentPlaceHolder1_countryDDL_CountryDropDownList", "label=Belgium");
            testUtil.WaitForPageToLoad(selenium, "6000");
            testUtil.EnterCheckOut1DataAndSubmit(selenium);

            //On CheckOut2 at this point
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_countryDDL_CountryDropDownList", "Belgium"));

            selenium.Click("ctl00_ContentPlaceHolder1_BackButton");
            testUtil.WaitForPageToLoad(selenium, "6000");

            //On CheckOut1 at this point
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_countryDDL_CountryDropDownList", "Belgium"));

            selenium.Click("ctl00_ContentPlaceHolder1_BackButton");
            testUtil.WaitForPageToLoad(selenium, "6000");

            //On ViewCart at this point
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry", "Belgium"));
        }

        public void EnsureCurrencySetFromBillingCountryTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);

            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Austria", "Euros");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Australia", "Australian Dollars");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Belgium", "Euros");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Canada", "Canadian Dollars");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Denmark", "Euros");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Finland", "Euros");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "France", "Euros");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Germany", "Euros");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Greece", "Euros");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Hong Kong", "Hong Kong Dollars");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Ireland", "Euros");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Italy", "Euros");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Japan", "Japanese Yen");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Netherlands", "Euros");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "New Zealand", "NZ Dollars");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Singapore", "Singaporian Dollars");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Spain", "Euros");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "Sweden", "Swedish Kroners");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "United Kingdom", "UK Pounds");
            testUtil.CurrencySetFromBillingCountryHelper(selenium, "United States", "US Dollars");
        }
    }
}