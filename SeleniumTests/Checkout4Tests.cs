using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests {
    [TestFixture]
    public class Checkout4Tests {
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
        public void LoadCheckout4Test() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);

            testUtil.EnterCheckOut1DataAndSubmit(selenium);
            testUtil.EnterCheckOut2DataAndSubmit(selenium);
            testUtil.EnterCheckOut3DataAndSubmit(selenium);

            string htmlSource = selenium.GetHtmlSource();
            //Cart Details
            Assert.IsTrue(htmlSource.Contains("<legend>Cart Details</legend>"));
            Assert.IsTrue(htmlSource.Contains("4. Please review and place your order"));
            Assert.IsTrue(htmlSource.Contains("Brush From CatzRUs In Yellow"));
            Assert.IsTrue(htmlSource.Contains("<br>Size: Large"));
            Assert.IsTrue(htmlSource.Contains("<td colspan=\"3\" align=\"left\">Shipping EXPRESS</td>"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: center;\" valign=\"middle\">1</td>"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: right; height: 25px;\">Total (Inc GST)</td>"));

            //Shipping Details
            Assert.IsTrue(htmlSource.Contains("<legend>Shipping Details</legend>"));
            Assert.IsTrue(htmlSource.Contains("<th>Name:</th>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ShippingNameLabel\">Felicity&nbsp;Davies</span>"));
            Assert.IsTrue(htmlSource.Contains("<th>Address:</th>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ShippingAddressLabel\">10 Smith Street</span>"));
            Assert.IsTrue(htmlSource.Contains("<th>Suburb/City:</th>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ShippingSuburbLabel\">SmithTown</span>"));
            Assert.IsTrue(htmlSource.Contains("<th>State/Province/Region:</th>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ShippingStateLabel\">NSW</span>"));
            Assert.IsTrue(htmlSource.Contains("<th>Zip/Postcode:</th>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ShippingPostcodeLabel\">2000</span>"));
            Assert.IsTrue(htmlSource.Contains("<th>Country:</th>"));
            Assert.IsTrue(htmlSource.Contains("Hong Kong"));

            //Payment Details
            Assert.IsTrue(htmlSource.Contains("<legend>Payment Details</legend>"));
            Assert.IsTrue(htmlSource.Contains("<th>Payment Method:</th>"));
            //Assert.IsTrue(htmlSource.Contains("<td align=\"left\">Credit Card</td>"));
            Assert.IsTrue(htmlSource.Contains("<th>Name on Card:</th>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CardholderLabel\">Andre Valleta</span>"));
            Assert.IsTrue(htmlSource.Contains("<th>Card Number:</th>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CardNumberLabel\">12344</span>"));

            //Comments
            Assert.IsTrue(htmlSource.Contains("<legend>Comments</legend>"));
            Assert.IsTrue(htmlSource.Contains("<textarea name=\"ctl00$ContentPlaceHolder1$CommentTextBox\" rows=\"5\" cols=\"80\" id=\"ctl00_ContentPlaceHolder1_CommentTextBox\"></textarea>"));

            //GiftTag not present
            Assert.IsFalse(selenium.GetHtmlSource().Contains("<fieldset id=\"GiftTagFieldSet\" class=\"CheckOut\">"));
            Assert.IsFalse(selenium.GetHtmlSource().Contains("<legend>Gift Tag Message</legend>"));
        }

        [Test]
        public void ClickTermsAndConditionsLinkTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);

            testUtil.EnterCheckOut1DataAndSubmit(selenium);
            testUtil.EnterCheckOut2DataAndSubmit(selenium);
            testUtil.EnterCheckOut3DataAndSubmit(selenium);

            testUtil.WaitForPageToLoad(selenium, "7000");
            selenium.Click("ctl00_ContentPlaceHolder1_PolicyHyperlink");
            testUtil.WaitForPageToLoad(selenium, "7000");
            testUtil.MoveToPopUp(selenium, "10000");

            string htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("<h2><a name=\"Exchanges\"></a>Exchanges</h2>"));
            Assert.IsTrue(htmlSource.Contains("<h2><a name=\"Cancellations\"></a>Cancellations</h2>"));

            Assert.IsTrue(htmlSource.Contains("<h2><a name=\"Disclaimer\"></a>Disclaimer</h2>"));

            Assert.IsTrue(htmlSource.Contains("<h2><a name=\"Confidentiality\"></a>Confidentiality</h2>"));
            Assert.IsTrue(htmlSource.Contains("<h2><a name=\"Security\"></a>Security</h2>"));
            Assert.IsTrue(htmlSource.Contains("<h2><a name=\"WebSitePolsAndConds\"></a>Website Policies &amp; Conditions</h2>"));
            Assert.IsTrue(htmlSource.Contains("<h2><a name=\"Cookies\"></a>Cookies</h2>"));
        }

        [Test]
        public void GiftTagFieldPresentTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);

            //Add Gift Wrapping to Cart
            selenium.Click("ctl00_ContentPlaceHolder1_GiftWrapHyperlink");
            testUtil.WaitForPageToLoad(selenium, "2000");
            selenium.Click("ctl00_ContentPlaceHolder1_ProductFormView_AddToCartImageButton");
            testUtil.WaitForPageToLoad(selenium, "2000");
            testUtil.EnterCheckOutPages(selenium);

            testUtil.EnterCheckOut1DataAndSubmit(selenium);
            testUtil.EnterCheckOut2DataAndSubmit(selenium);
            testUtil.EnterCheckOut3DataAndSubmit(selenium);

            testUtil.WaitForPageToLoad(selenium, "2000");
            //Assert.IsTrue(selenium.GetHtmlSource().Contains("<fieldset id=\"GiftTagFieldSet\" class=\"CheckOut\">"));
            Assert.IsTrue(selenium.GetHtmlSource().Contains("<legend>Gift Tag Message</legend>"));
        }

        [Test]
        public void GiftTagFieldNotPresentTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);

            testUtil.EnterCheckOut1DataAndSubmit(selenium);
            testUtil.EnterCheckOut2DataAndSubmit(selenium);
            testUtil.EnterCheckOut3DataAndSubmit(selenium);

            Assert.IsFalse(selenium.GetHtmlSource().Contains("<fieldset id=\"GiftTagFieldSet\" class=\"CheckOut\">"));
            Assert.IsFalse(selenium.GetHtmlSource().Contains("<legend>Gift Tag Message</legend>"));
        }

        [Test]
        public void PaymentDetailsChangeTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.EnterCheckOutPages(selenium);
            testUtil.EnterCheckOut1DataAndSubmit(selenium);
            testUtil.EnterCheckOut2DataAndSubmit(selenium);
            testUtil.EnterCheckOut3DataAndSubmit(selenium);

            //At checkout4 page
            selenium.Click("ctl00_ContentPlaceHolder1_BackButton");
            testUtil.WaitForPageToLoad(selenium, "3000");

            //At checkout3 page
            testUtil.EnterDifferentCheckOut3DataAndSubmit(selenium);

            //Back at checkout4 page - assert changed payment details
            string htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("<th>Payment Method:</th>"));
            //Assert.IsTrue(htmlSource.Contains("<td align=\"left\">Credit Card</td>"));
            Assert.IsTrue(htmlSource.Contains("<th>Name on Card:</th>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CardholderLabel\">Bill Dodds</span>"));
            Assert.IsTrue(htmlSource.Contains("<th>Card Number:</th>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CardNumberLabel\">4444333322221111</span>"));
        }
    }
}