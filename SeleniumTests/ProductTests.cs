using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests {
    [TestFixture]
    public class ProductTests {
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
        public void Product1SelectTest() {
            testUtil.ProductPageHelper(selenium);

            Assert.AreEqual("Dogs - Grooming - Brush", selenium.GetTitle());
            Assert.IsTrue(selenium.IsTextPresent("Bookmark this page"));
            //Assert.IsTrue(selenium.IsTextPresent("Dummy Image"));
            Assert.IsTrue(selenium.IsTextPresent("Exchange and Returns Policy"));
            Assert.IsTrue(selenium.IsTextPresent("Payment Methods"));
            Assert.IsTrue(selenium.IsTextPresent("Digg"));
            Assert.IsTrue(selenium.IsTextPresent("Reddit"));
            Assert.IsTrue(selenium.IsTextPresent("Google"));
            Assert.IsTrue(selenium.IsTextPresent("Email"));
            Assert.IsTrue(selenium.IsTextPresent("del.icio.us"));
            Assert.IsTrue(selenium.IsTextPresent("PetsPlayground will exchange your product for a different size or refund your money if it is return within 30 days of receipt. Please see the Policies page for more information."));
            Assert.IsTrue(selenium.IsTextPresent("We accept payment via Visa, MasterCard, American Express and Paypal."));

            selenium.Select("ctl00_ContentPlaceHolder1_ProductFormView_productSizeDDL_SizeDropDownList", "label=Medium");
            Assert.AreEqual("Medium", selenium.GetSelectedLabel("ctl00_ContentPlaceHolder1_ProductFormView_productSizeDDL_SizeDropDownList"));
            selenium.Select("ctl00_ContentPlaceHolder1_ProductFormView_productColorDDL_ColorDropDownList", "label=Red");
            Assert.AreEqual("Red", selenium.GetSelectedLabel("ctl00_ContentPlaceHolder1_ProductFormView_productColorDDL_ColorDropDownList"));
            Assert.AreEqual("1", selenium.GetSelectedValue("ctl00_ContentPlaceHolder1_ProductFormView_productQuantityDDL_QuantityDropDownList"));
        }

        [Test]
        public void DiggLinkTest() {
            testUtil.ProductPageHelper(selenium);
            selenium.Click("ctl00_ContentPlaceHolder1_ProductFormView_DiggHyperLink");
            testUtil.MoveToPopUp(selenium, "7000");

            Assert.IsTrue(selenium.GetLocation().Equals("http://digg.com/submit?phase=2&url=http://www.mywipstore.com.au/BrowseItem.aspx?ProdID=10&title=Brush"));
        }

        [Test]
        public void RedditLinkTest() {
            testUtil.ProductPageHelper(selenium);
            selenium.Click("ctl00_ContentPlaceHolder1_ProductFormView_RedditHyperLink");
            testUtil.MoveToPopUp(selenium, "7000");

            Assert.IsTrue(selenium.GetLocation().Equals("http://www.reddit.com/login?dest=%2Fsubmit%3Furl%3Dhttp%253A%252F%252Fwww.mywipstore.com.au%252FBrowseItem.aspx%253FProdID%253D10%26title%3DBrush"));
        }

        [Test]
        public void GoogleLinkTest() {
            testUtil.ProductPageHelper(selenium);
            selenium.Click("ctl00_ContentPlaceHolder1_ProductFormView_GoogleHyperLink");
            testUtil.MoveToPopUp(selenium, "7000");

            Assert.IsTrue(
                selenium.GetLocation().Equals(
                    "https://www.google.com/bookmarks/mark?op=edit&output=popup&bkmk=http://www.mywipstore.com.au/BrowseItem.aspx?ProdID=10&title=Brush")
                    ? selenium.GetLocation().Equals(
                        "https://www.google.com/bookmarks/mark?op=edit&output=popup&bkmk=http://www.mywipstore.com.au/BrowseItem.aspx?ProdID=10&title=Brush")
                    : selenium.GetLocation().Equals(
                        "https://www.google.com/accounts/ServiceLogin?hl=en&continue=https://www.google.com/bookmarks/mark%3Fop%3Dedit%26output%3Dpopup%26bkmk%3Dhttp://www.mywipstore.com.au/BrowseItem.aspx%3FProdID%3D10%26title%3DBrush&nui=1&service=bookmarks"));
        }

        //[Test]
        //public void DeliciousLinkTest() {
        //    testUtil.ProductPageHelper(selenium);
        //    selenium.Click("ctl00_ContentPlaceHolder1_ProductFormView_DeliciousHyperLink");
        //    testUtil.MoveToPopUp(selenium, "5000");

        //    //Console.WriteLine("DelLink: " + selenium.GetLocation());
        //    Assert.IsTrue(selenium.GetLocation().Equals("https://secure.delicious.com/login?v=5&jump=http%3A%2F%2Fdelicious.com%2Fsave%3Furl%3Dhttp%253A%252F%252Fwww.mywipstore.com.au%252FBrowseItem.aspx%253FProdID%253D10%26title%3DBrush%26notes%3D%26tags%3D%26noui%3Dno%26share%3Dyes%26jump%3Dyes%26time%3D1252236311%26recipients%3D"));
        //}
    }
}