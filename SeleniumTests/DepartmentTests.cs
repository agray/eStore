using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests {
    [TestFixture]
    public class DepartmentTests {
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
            testUtil.WaitForPageToLoad(selenium, "5000");
            selenium.Click("ctl00_NavCatalogue_DepartmentRepeater_ctl00_DepartmentHyperLink");
            testUtil.WaitForPageToLoad(selenium, "2000");

            Assert.AreEqual("Dogs", selenium.GetTitle());
            Assert.IsTrue(selenium.IsTextPresent("Grooming"));
            Assert.IsTrue(selenium.IsTextPresent("BathTime"));
            Assert.IsTrue(selenium.IsTextPresent("newProduct"));
            //Assert.IsTrue(selenium.IsTextPresent("Snakes"));
            //Assert.IsTrue(selenium.IsTextPresent("Possums"));
            //Assert.IsTrue(selenium.IsTextPresent("spiders"));
        }

        [Test]
        public void DepartmentChangeCurrencyTest() {
            testUtil.OpenHomePage(selenium);
            selenium.Click("ctl00_NavCatalogue_DepartmentRepeater_ctl00_DepartmentHyperLink");
            testUtil.WaitForPageToLoad(selenium, "2000");

            //Set Currency to Canadian Dollars
            testUtil.ChangeCurrencyHelper(selenium, "Canadian Dollars", "2");
            testUtil.AssertDepartments(selenium, "CAD", "$");

            //Set Currency to Australian dollars
            testUtil.ChangeCurrencyHelper(selenium, "Australian Dollars", "1");
            testUtil.AssertDepartments(selenium, "AUD", "$");

            //Set Currency to Danish Kroners
            testUtil.ChangeCurrencyHelper(selenium, "Danish Kroners", "3");
            testUtil.AssertDepartments(selenium, "DKK", "");

            //Set Currency to Hong Kong Dollars
            testUtil.ChangeCurrencyHelper(selenium, "Hong Kong Dollars", "5");
            testUtil.AssertDepartments(selenium, "HKD", "$");

            //Set Currency to Japanese Yen
            testUtil.ChangeCurrencyHelper(selenium, "Japanese Yen", "6");
            testUtil.AssertDepartments(selenium, "JPY", "¥");

            //Set Currency to NZ Dollars
            testUtil.ChangeCurrencyHelper(selenium, "NZ Dollars", "7");
            testUtil.AssertDepartments(selenium, "NZD", "$");

            //Set Currency to Singapore Dollar
            testUtil.ChangeCurrencyHelper(selenium, "Singaporian Dollars", "8");
            testUtil.AssertDepartments(selenium, "SGD", "$");

            //Set Currency to Swedish Kroners
            testUtil.ChangeCurrencyHelper(selenium, "Swedish Kroners", "9");
            testUtil.AssertDepartments(selenium, "SEK", "");

            //Set Currency to UK Pounds
            testUtil.ChangeCurrencyHelper(selenium, "UK Pounds", "10");
            testUtil.AssertDepartments(selenium, "GBP", "£");

            //Set Currency to US Dollars
            testUtil.ChangeCurrencyHelper(selenium, "US Dollars", "30");
            testUtil.AssertDepartments(selenium, "USD", "$");

            //Set Currency to Euros
            testUtil.ChangeCurrencyHelper(selenium, "Euros", "31");
            testUtil.AssertDepartments(selenium, "EUR", "€");
        }

        [Test]
        public void HomeBreadcrumbTest() {
            testUtil.OpenHomePage(selenium);
            selenium.Click("ctl00_NavCatalogue_DepartmentRepeater_ctl00_DepartmentHyperLink");
            testUtil.WaitForPageToLoad(selenium, "2000");

            selenium.Click("ctl00_ContentPlaceHolder1_Home_Breadcrumb");
            testUtil.WaitForPageToLoad(selenium, "2000");

            Assert.IsTrue(selenium.GetLocation().Equals("http://www.mywipstore.com.au/"));
        }
    }
}