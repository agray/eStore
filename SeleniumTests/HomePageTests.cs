using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests {
    [TestFixture]
    public class HomePageTests {
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
        public void Department1SelectTest() {
            selenium.Open("/");
            selenium.WaitForPageToLoad("5000");
            selenium.Click("ctl00_NavCatalogue_DepartmentRepeater_ctl00_DepartmentHyperLink");
            selenium.WaitForPageToLoad("2000");

            Assert.AreEqual("Dogs", selenium.GetTitle());
            Assert.IsTrue(selenium.IsTextPresent("Grooming"));
            Assert.IsTrue(selenium.IsTextPresent("BathTime"));
            Assert.IsTrue(selenium.IsTextPresent("newProduct"));
            //Assert.IsTrue(selenium.IsTextPresent("Snakes"));
            //Assert.IsTrue(selenium.IsTextPresent("Possums"));
            //Assert.IsTrue(selenium.IsTextPresent("spiders"));
        }

        [Test]
        public void HomePageChangeCurrencyTest() {
            selenium.Open("/");
            selenium.WaitForPageToLoad("5000");

            //Set Currency to Canadian Dollars
            testUtil.ChangeCurrencyHelper(selenium, "Canadian Dollars", "2");
            testUtil.AssertNavCartValues(selenium, "$", "0.00", "0.00", "CAD");

            //Set Currency to Australian dollars
            testUtil.ChangeCurrencyHelper(selenium, "Australian Dollars", "1");
            testUtil.AssertNavCartValues(selenium, "$", "0.00", "0.00", "AUD");

            //Set Currency to Danish Kroners
            testUtil.ChangeCurrencyHelper(selenium, "Danish Kroners", "3");
            testUtil.AssertNavCartValues(selenium, "", "0,00", "0,00", "DKK");

            //Set Currency to Hong Kong Dollars
            testUtil.ChangeCurrencyHelper(selenium, "Hong Kong Dollars", "5");
            testUtil.AssertNavCartValues(selenium, "$", "0.00", "0.00", "HKD");

            //Set Currency to Japanese Yen
            testUtil.ChangeCurrencyHelper(selenium, "Japanese Yen", "6");
            testUtil.AssertNavCartValues(selenium, "¥", "0", "0", "JPY");

            //Set Currency to NZ Dollars
            testUtil.ChangeCurrencyHelper(selenium, "NZ Dollars", "7");
            testUtil.AssertNavCartValues(selenium, "$", "0.00", "0.00", "NZD");

            //Set Currency to Singapore Dollar
            testUtil.ChangeCurrencyHelper(selenium, "Singaporian Dollars", "8");
            testUtil.AssertNavCartValues(selenium, "$", "0.00", "0.00", "SGD");

            //Set Currency to Swedish Kroners
            testUtil.ChangeCurrencyHelper(selenium, "Swedish Kroners", "9");
            testUtil.AssertNavCartValues(selenium, "", "0,00", "0,00", "SEK");

            //Set Currency to UK Pounds
            testUtil.ChangeCurrencyHelper(selenium, "UK Pounds", "10");
            testUtil.AssertNavCartValues(selenium, "£", "0.00", "0.00", "GBP");

            //Set Currency to US Dollars
            testUtil.ChangeCurrencyHelper(selenium, "US Dollars", "30");
            testUtil.AssertNavCartValues(selenium, "$", "0.00", "0.00", "USD");

            //Set Currency to Euros
            testUtil.ChangeCurrencyHelper(selenium, "Euros", "31");
            testUtil.AssertNavCartValues(selenium, "€", "0,00", "0,00", "EUR");
        }
    }
}