using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests {
    [TestFixture]
    public class Checkout2Tests {
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
        public void LoadCheckout2Test() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);

            testUtil.EnterCheckOut1DataAndSubmit(selenium);

            Assert.IsTrue(selenium.GetHtmlSource().Contains("2. Enter your shipping details"));
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_sameAddressDDL_YesNoDropDownList", "Yes"));
        }

        [Test]
        public void Checkout2RequiredFieldTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);
            testUtil.EnterCheckOut1DataAndSubmit(selenium);

            selenium.Select("ctl00_ContentPlaceHolder1_sameAddressDDL_YesNoDropDownList", "label=No");
            testUtil.WaitForPageToLoad(selenium, "5000");
            selenium.Click("ctl00_ContentPlaceHolder1_NextButton");
            testUtil.WaitForPageToLoad(selenium, "5000");

            testUtil.AssertCheckout2RequiredFieldRedLabels(selenium, "inline");
        }

        [Test]
        public void Checkout2FieldsClearedTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);
            testUtil.EnterCheckOut1DataAndSubmit(selenium);

            string preExistingCountry = selenium.GetSelectedLabel("ctl00_ContentPlaceHolder1_countryDDL_CountryDropDownList");

            selenium.Select("ctl00_ContentPlaceHolder1_sameAddressDDL_YesNoDropDownList", "label=No");
            testUtil.WaitForPageToLoad(selenium, "3000");

            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShippingFirstNameTextBox", ""));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShippingLastNameTextBox", ""));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShippingAddressTextBox", ""));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShippingSuburbTextBox", ""));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShippingStateTextBox", ""));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShippingPostcodeTextBox", ""));
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_countryDDL_CountryDropDownList", preExistingCountry));
        }

        [Test]
        public void Checkout2FieldsSetBackToCO1ValuesTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);
            testUtil.EnterCheckOut1DataAndSubmit(selenium);

            string preExistingFirstname = selenium.GetValue("ctl00_ContentPlaceHolder1_ShippingFirstNameTextBox");
            string preExistingLastname = selenium.GetValue("ctl00_ContentPlaceHolder1_ShippingLastNameTextBox");
            string preExistingAddress = selenium.GetValue("ctl00_ContentPlaceHolder1_ShippingAddressTextBox");
            string preExistingSuburb = selenium.GetValue("ctl00_ContentPlaceHolder1_ShippingSuburbTextBox");
            string preExistingState = selenium.GetValue("ctl00_ContentPlaceHolder1_ShippingStateTextBox");
            string preExistingPostcode = selenium.GetValue("ctl00_ContentPlaceHolder1_ShippingPostcodeTextBox");
            string preExistingCountry = selenium.GetSelectedLabel("ctl00_ContentPlaceHolder1_countryDDL_CountryDropDownList");

            selenium.Select("ctl00_ContentPlaceHolder1_sameAddressDDL_YesNoDropDownList", "label=No");
            testUtil.WaitForPageToLoad(selenium, "5000");
            selenium.Select("ctl00_ContentPlaceHolder1_sameAddressDDL_YesNoDropDownList", "label=Yes");
            testUtil.WaitForPageToLoad(selenium, "5000");

            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShippingFirstNameTextBox", preExistingFirstname));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShippingLastNameTextBox", preExistingLastname));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShippingAddressTextBox", preExistingAddress));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShippingSuburbTextBox", preExistingSuburb));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShippingStateTextBox", preExistingState));
            Assert.IsTrue(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_ShippingPostcodeTextBox", preExistingPostcode));
            Assert.IsTrue(testUtil.SelectValueEqual(selenium, "ctl00_ContentPlaceHolder1_countryDDL_CountryDropDownList", preExistingCountry));

            testUtil.AssertCheckout2RequiredFieldRedLabels(selenium, "none");
        }

        [Test]
        public void Checkout2CheckOut1FieldsRecordedSeparatelyTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);
            testUtil.EnterCheckOut1DataAndSubmit(selenium);
            testUtil.EnterCheckOut2DataAndSubmit(selenium);

            //Go back to Checkout2 Page
            selenium.Click("ctl00_ContentPlaceHolder1_BackButton");
            testUtil.WaitForPageToLoad(selenium, "10000");

            //Remember Checkout2 Fields
            string CO2Firstname = selenium.GetValue("ctl00_ContentPlaceHolder1_ShippingFirstNameTextBox");
            string CO2Lastname = selenium.GetValue("ctl00_ContentPlaceHolder1_ShippingLastNameTextBox");
            string CO2Address = selenium.GetValue("ctl00_ContentPlaceHolder1_ShippingAddressTextBox");
            string CO2Suburb = selenium.GetValue("ctl00_ContentPlaceHolder1_ShippingSuburbTextBox");
            string CO2State = selenium.GetValue("ctl00_ContentPlaceHolder1_ShippingStateTextBox");
            string CO2Postcode = selenium.GetValue("ctl00_ContentPlaceHolder1_ShippingPostcodeTextBox");
            string CO2Country = selenium.GetSelectedLabel("ctl00_ContentPlaceHolder1_countryDDL_CountryDropDownList");

            //Go back to Checkout1 Page
            selenium.Click("ctl00_ContentPlaceHolder1_BackButton");
            testUtil.WaitForPageToLoad(selenium, "6000");

            //Assert that Checkout1 and Checkout2 values do not match
            Assert.IsFalse(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_CustomerFirstNameTextBox", CO2Firstname));
            Assert.IsFalse(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_CustomerLastNameTextBox", CO2Lastname));
            Assert.IsFalse(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_CustomerAddressTextBox", CO2Address));
            Assert.IsFalse(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_CustomerSuburbTextBox", CO2Suburb));
            Assert.IsFalse(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_CustomerStateTextBox", CO2State));
            Assert.IsFalse(testUtil.TextValueEqual(selenium, "ctl00_ContentPlaceHolder1_CustomerPostcodeTextBox", CO2Postcode));
        }
    }
}