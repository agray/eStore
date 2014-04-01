using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests {
    [TestFixture]
    public class CategoryTests {
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
        public void Category1SelectTest() {
            testUtil.OpenHomePage(selenium);
            selenium.Click("ctl00_NavCatalogue_DepartmentRepeater_ctl00_CategoryRepeater_ctl00_CategoryHyperLink");
            testUtil.WaitForPageToLoad(selenium, "2000");

            string htmlSource = selenium.GetHtmlSource();
            Assert.AreEqual("Dogs - Grooming", selenium.GetTitle());
            Assert.IsTrue(selenium.IsTextPresent("Grooming"));
            Assert.IsTrue(htmlSource.Contains("Keeps your pet clean and fresh year round."));
            Assert.IsTrue(htmlSource.Contains("25"));
            Assert.IsTrue(htmlSource.Contains(" products (showing 1 - 12)"));
        }

        [Test]
        public void HomeBreadcrumbTest() {
            testUtil.OpenHomePage(selenium);
            selenium.Click("ctl00_NavCatalogue_DepartmentRepeater_ctl00_CategoryRepeater_ctl00_CategoryHyperLink");
            testUtil.WaitForPageToLoad(selenium, "2000");

            selenium.Click("ctl00_ContentPlaceHolder1_HomeHyperlink");
            testUtil.WaitForPageToLoad(selenium, "2000");

            Assert.IsTrue(selenium.GetLocation().Equals("http://www.mywipstore.com.au/"));
        }

        [Test]
        public void DepartmentBreadcrumbTest() {
            testUtil.OpenHomePage(selenium);
            selenium.Click("ctl00_NavCatalogue_DepartmentRepeater_ctl00_CategoryRepeater_ctl00_CategoryHyperLink");
            testUtil.WaitForPageToLoad(selenium, "2000");

            selenium.Click("ctl00_ContentPlaceHolder1_DepartmentName_Breadcrumb");
            testUtil.WaitForPageToLoad(selenium, "2000");

            Assert.IsTrue(selenium.GetLocation().Equals("http://www.mywipstore.com.au/BrowseDepartment.aspx?DepID=1"));
        }
    }
}