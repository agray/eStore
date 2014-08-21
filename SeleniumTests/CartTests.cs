using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests {
    [TestFixture]
    public class CartTests {
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
            Assert.AreEqual(String.Empty, verificationErrors.ToString());
        }

        [Test]
        public void AddOneItemTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);

            var htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("Brush From CatzRUs"));
            Assert.IsTrue(htmlSource.Contains("Size: Large"));
            Assert.IsTrue(htmlSource.Contains("Color: Yellow"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: center;\" valign=\"middle\">1</td>"));
            Assert.IsTrue(htmlSource.Contains("Price(<span>AUD</span>)"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_UnitPriceLabel\">$80.00</span>"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: right; height: 25px;\">$93.20</td>"));
            Assert.IsTrue(htmlSource.Contains("Total (Inc GST)"));
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "13.20", "AUD");
        }

        [Test]
        public void AddExactSameItemTwiceTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);

            selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_ItemImage");
            testUtil.WaitForPageToLoad(selenium, "5000");
            testUtil.AddItemToCart(selenium);

            var htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("Brush From CatzRUs"));
            Assert.IsTrue(htmlSource.Contains("Size: Large"));
            Assert.IsTrue(htmlSource.Contains("Color: Yellow"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: center;\" valign=\"middle\">2</td>"));
            Assert.IsTrue(htmlSource.Contains("Price(<span>AUD</span>)"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_UnitPriceLabel\">$160.00</span>"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: right; height: 25px;\">$186.40</td>"));
            Assert.IsTrue(htmlSource.Contains("Total (Inc GST)"));
            testUtil.AssertNavCartValues(selenium, "$", "160.00", "26.40", "AUD");
        }

        [Test]
        public void AddSameItemDifferentAttributesTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_ItemImage");
            testUtil.WaitForPageToLoad(selenium, "2000");
            selenium.Select("ctl00_ContentPlaceHolder1_ProductFormView_productSizeDDL_SizeDropDownList", "label=Medium");
            selenium.Select("ctl00_ContentPlaceHolder1_ProductFormView_productColorDDL_ColorDropDownList", "label=Red");
            testUtil.AddItemToCart(selenium);

            var htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("Brush From CatzRUs"));
            Assert.IsTrue(htmlSource.Contains("<br>Size: Large"));
            Assert.IsTrue(htmlSource.Contains("<br>Color: Yellow"));
            Assert.IsTrue(htmlSource.Contains("Price(<span>AUD</span>)"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_UnitPriceLabel\">$80.00</span>"));
            Assert.IsTrue(htmlSource.Contains("<br>Size: Medium"));
            Assert.IsTrue(htmlSource.Contains("<br>Color: Red"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl01_UnitPriceLabel\">$80.00</span>"));
            Assert.IsTrue(htmlSource.Contains("Ship 3 kg to"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: right; height: 25px;\">$186.40</td>"));
            Assert.IsTrue(htmlSource.Contains("Total (Inc GST)"));
            testUtil.AssertNavCartValues(selenium, "$", "160.00", "26.40", "AUD");
        }

        [Test]
        public void AddOneRemoveOneItemTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            testUtil.RemoveFirstItemHelper(selenium);
            Assert.IsTrue(selenium.GetHtmlSource().Contains("Your Cart is Empty."));
        }

        [Test]
        public void AddExactSameItemTwiceThenRemoveTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_ItemImage");
            testUtil.WaitForPageToLoad(selenium, "2000");
            testUtil.AddItemToCart(selenium);
            testUtil.RemoveFirstItemHelper(selenium);
            Assert.IsTrue(selenium.GetHtmlSource().Contains("Your Cart is Empty."));
        }

        [Test]
        public void AddSameItemDifferentAttributesThenRemoveOneTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);
            selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_ItemImage");
            testUtil.WaitForPageToLoad(selenium, "2000");
            selenium.Select("ctl00_ContentPlaceHolder1_ProductFormView_productSizeDDL_SizeDropDownList", "label=Small");
            selenium.Select("ctl00_ContentPlaceHolder1_ProductFormView_productColorDDL_ColorDropDownList", "label=Orange");
            selenium.Click("ctl00_ContentPlaceHolder1_ProductFormView_AddToCartImageButton");
            testUtil.WaitForPageToLoad(selenium, "2000");
            testUtil.RemoveFirstItemHelper(selenium);

            var htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("Brush From CatzRUs"));
            Assert.IsTrue(htmlSource.Contains("<br>Size: Small"));
            Assert.IsTrue(htmlSource.Contains("<br>Color: Orange"));
            Assert.IsTrue(htmlSource.Contains("Price(<span>AUD</span>)"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_UnitPriceLabel\">$80.00</span>"));
            Assert.IsTrue(htmlSource.Contains("Ship 1.5 kg to"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: right; height: 25px;\">$93.20</td>"));
            Assert.IsTrue(htmlSource.Contains("Total (Inc GST)"));
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "13.20", "AUD");
        }

        [Test]
        public void calculateShippingCostsTest_NoWeight() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddWrappingItemHelper(selenium);

            testUtil.AssertViewCartTableFigures(selenium, "$", "6.25", "0.00");
        }

        [Test]
        public void ChangeShippingCountryTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.WaitForPageToLoad(selenium, "2000");
            testUtil.AddOneItemHelper(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Austria", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[2]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            //Console.WriteLine(selenium.GetHtmlSource());
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "Belgium", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[3]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "Australia", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[1]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "13.20", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "93.20", "13.20");

            testUtil.ChangeShippingCountryHelper(selenium, "Canada", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[4]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "36.55", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "116.55", "36.55");

            testUtil.ChangeShippingCountryHelper(selenium, "Denmark", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[5]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "Finland", "//option[@value='11']");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "France", "//option[@value='12']");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "Germany", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[8]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "Greece", "//option[@value='13']");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "Hong Kong", "//option[@value='14']");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "30.25", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "110.25", "30.25");

            testUtil.ChangeShippingCountryHelper(selenium, "Ireland", "//option[@value='15']");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "Italy", "//option[@value='16']");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "Japan", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[13]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "30.25", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "110.25", "30.25");

            testUtil.ChangeShippingCountryHelper(selenium, "Netherlands", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[14]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "New Zealand", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[15]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "23.85", "AUD");

            testUtil.ChangeShippingCountryHelper(selenium, "Singapore", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[16]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "30.25", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "110.25", "30.25");

            testUtil.ChangeShippingCountryHelper(selenium, "Spain", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[17]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "Sweden", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[18]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "United Kingdom", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[19]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");

            testUtil.ChangeShippingCountryHelper(selenium, "United States", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[20]");
            testUtil.AssertNavCartValues(selenium, "$", "80.00", "36.55", "AUD");
            testUtil.AssertViewCartTableFigures(selenium, "$", "116.55", "36.55");
        }

        [Test]
        public void ChangeShippingModeTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.WaitForPageToLoad(selenium, "2000");
            testUtil.AddOneItemHelper(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Austria", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[2]");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Belgium", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[3]");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Australia", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[1]");
            testUtil.AssertZoneZ(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Canada", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[4]");
            testUtil.AssertZoneC(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Denmark", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[5]");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Finland", "//option[@value='11']");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "France", "//option[@value='12']");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Germany", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[8]");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Greece", "//option[@value='13']");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Hong Kong", "//option[@value='14']");
            testUtil.AssertZoneB(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Ireland", "//option[@value='15']");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Italy", "//option[@value='16']");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Japan", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[13]");
            testUtil.AssertZoneB(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Netherlands", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[14]");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "New Zealand", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[15]");
            testUtil.AssertZoneA(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Singapore", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[16]");
            testUtil.AssertZoneB(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Spain", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[17]");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "Sweden", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[18]");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "United Kingdom", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[19]");
            testUtil.AssertZoneD(selenium);

            testUtil.ChangeShippingCountryHelper(selenium, "United States", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[20]");
            testUtil.AssertZoneC(selenium);
        }

        [Test]
        public void ChangeCurrencyTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);

            testUtil.ChangeCurrencyHelper(selenium, "Hong Kong Dollars", "5");
            testUtil.AssertViewCartLabels(selenium, "$", "Total (Ex GST)", "HKD");

            testUtil.ChangeCurrencyHelper(selenium, "Australian Dollars", "1");
            testUtil.AssertViewCartLabels(selenium, "$", "Total (Inc GST)", "AUD");

            testUtil.ChangeCurrencyHelper(selenium, "Canadian Dollars", "2");
            testUtil.AssertViewCartLabels(selenium, "$", "Total (Ex GST)", "CAD");

            testUtil.ChangeCurrencyHelper(selenium, "Danish Kroners", "3");
            testUtil.AssertViewCartLabels(selenium, "", "Total (Ex GST)", "DKK");

            testUtil.ChangeCurrencyHelper(selenium, "Japanese Yen", "6");
            testUtil.AssertViewCartLabels(selenium, "¥", "Total (Ex GST)", "JPY");

            testUtil.ChangeCurrencyHelper(selenium, "NZ Dollars", "7");
            testUtil.AssertViewCartLabels(selenium, "$", "Total (Ex GST)", "NZD");

            testUtil.ChangeCurrencyHelper(selenium, "Singaporian Dollars", "8");
            testUtil.AssertViewCartLabels(selenium, "$", "Total (Ex GST)", "SGD");

            testUtil.ChangeCurrencyHelper(selenium, "Swedish Kroners", "9");
            testUtil.AssertViewCartLabels(selenium, "", "Total (Ex GST)", "SEK");

            testUtil.ChangeCurrencyHelper(selenium, "UK Pounds", "10");
            testUtil.AssertViewCartLabels(selenium, "£", "Total (Ex GST)", "GBP");

            testUtil.ChangeCurrencyHelper(selenium, "US Dollars", "30");
            testUtil.AssertViewCartLabels(selenium, "$", "Total (Ex GST)", "USD");

            testUtil.ChangeCurrencyHelper(selenium, "Euros", "31");
            testUtil.AssertViewCartLabels(selenium, "€", "Total (Ex GST)", "EUR");
        }

        [Test]
        public void ContinueShoppingTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);

            selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_ItemImage");
            testUtil.WaitForPageToLoad(selenium, "2000");
            Assert.IsTrue(selenium.GetHtmlSource().Contains("Brush"));
        }

        [Test]
        public void GiftWrappedTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);

            selenium.Click("ctl00_ContentPlaceHolder1_GiftWrapHyperlink");
            testUtil.WaitForPageToLoad(selenium, "2000");
            Assert.IsTrue(selenium.GetHtmlSource().Contains("Gift Wrapping"));
        }

        //[Test]
        //public void calculateShippingRateTest_ShippingFieldsBlank() {
        //    using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
        //        testUtil.OpenHomePage(selenium);
        //        testUtil.AddOneItemHelper(selenium);
        //        SessionHandler.ShippingCountry = "";
        //        SessionHandler.ShippingMode = "";

        //        Assert.AreEqual(13.2, SessionHandler.TotalShipping);
        //    }
        //}

        //[Test]
        //public void IsWrappingFalseOnEmptyCartTest() {
        //    using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
        //        testUtil.OpenHomePage(selenium);
        //        testUtil.AddOneItemHelper(selenium);
        //        testUtil.RemoveFirstItemHelper(selenium);
        //        Assert.IsTrue(SessionHandler.Wrapping == false);
        //    }
        //}

        //[Test]
        //public void IsWrappingTrueTest_WrappingAddedTwiceAndThenRemovedTest() {
        //    using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
        //        testUtil.OpenHomePage(selenium);
        //        testUtil.AddWrappingItemHelper(selenium);
        //        testUtil.AddWrappingItemHelper(selenium);
        //        testUtil.RemoveFirstItemHelper(selenium);
        //        Assert.IsTrue(SessionHandler.Wrapping == false);
        //    }
        //}

        //[Test]
        //public void IsWrappingTrueTest() {
        //    using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
        //        testUtil.OpenHomePage(selenium);
        //        testUtil.AddWrappingItemHelper(selenium);
        //        Assert.IsTrue(SessionHandler.Wrapping == true);
        //    }
        //}

        //[Test]
        //public void IsWrappingFalseTest() {
        //    using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
        //        testUtil.OpenHomePage(selenium);
        //        testUtil.AddOneItemHelper(selenium);
        //        Assert.IsTrue(SessionHandler.Wrapping == false);
        //    }
        //}

        [Test]
        public void HelpLinkTest() {
            testUtil.OpenHomePage(selenium);
            testUtil.AddOneItemHelper(selenium);

            selenium.Click("link=FAQ");
            testUtil.WaitForPageToLoad(selenium, "3000");

            var htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("<h1>Frequently Asked Questions</h1>"));
            Assert.IsTrue(htmlSource.Contains("<li><a href=\"#howpay\">How do I order and pay?</a></li>"));
            Assert.IsTrue(htmlSource.Contains("<li><a href=\"#payment\">My payment won't go through, what's wrong?</a></li>"));
            Assert.IsTrue(htmlSource.Contains("<li><a href=\"#shipping\">How long does the shipping take?</a></li>"));
            Assert.IsTrue(htmlSource.Contains("<li><a href=\"#duties\">Will there be customs duties, taxes, etc?</a></li>"));
            Assert.IsTrue(htmlSource.Contains("<li><a href=\"#outlets\">Are there any outlets where I can look at your products?</a></li>"));
            Assert.IsTrue(htmlSource.Contains("<li><a href=\"#exchange\">If I am dissatisfied can I get an exchange or refund?</a></li>"));
            Assert.IsTrue(htmlSource.Contains("<h2>How do I order and pay?</h2>"));
            Assert.IsTrue(htmlSource.Contains("<h2>My payment won't go through, what's wrong?</h2>"));
            Assert.IsTrue(htmlSource.Contains("<h2>How long does the shipping take?</h2>"));
            Assert.IsTrue(htmlSource.Contains("<h2>Will there be customs duties, taxes, etc?</h2>"));
            Assert.IsTrue(htmlSource.Contains("<h2>Are there any outlets where I can look at your products?</h2>"));
            Assert.IsTrue(htmlSource.Contains("<h2>If I am dissatisfied can I get an exchange or refund?</h2>"));
            Assert.IsTrue(htmlSource.Contains("<h2>Prices</h2>"));
            Assert.IsTrue(htmlSource.Contains("<h2>Out of Stock</h2>"));
            Assert.IsTrue(htmlSource.Contains("<h2>Company Information</h2>"));
            Assert.IsTrue(htmlSource.Contains("<li>The web browser being used does not have cookies enabled.</li>"));
            Assert.IsTrue(htmlSource.Contains("<li>The credit card being used does not have the international transactions facility enabled.</li>"));
            Assert.IsTrue(htmlSource.Contains("<li>The payment system has timed out.  The system will cut off your payment connection if it has been idle for one hour.</li>"));
            Assert.IsTrue(htmlSource.Contains("<li>Enable cookies in your browser (this setting is usually found in the browser options).</li>"));
            Assert.IsTrue(htmlSource.Contains("<li>Contact your bank to ensure international transactions are enabled for your credit card.</li>"));
            Assert.IsTrue(htmlSource.Contains("<li>Go to the view cart section again to reinitiate the payment process.</li>"));
            Assert.IsTrue(htmlSource.Contains("<li>Try another credit card.  It sounds obvious but it sometimes does the trick.</li>"));
            Assert.IsTrue(htmlSource.Contains("<li>Try another computer, this also sounds very obvious but sometimes the security settings on a computer system can prevent the successful completion of a transaction.</li>"));
            Assert.IsTrue(htmlSource.Contains("<li>If all else fails, record the error message and e-mail us at <a id=\"ctl00_ContentPlaceHolder1_ContactHyperlink1\" href=\"mailto:ContactUs@PetsPlayground.com.au\">ContactUs@PetsPlayground.com.au</a> with all the details that we may further assist you.</li>"));
            Assert.IsTrue(htmlSource.Contains("<li>Airmail - approximately 10-25 working days from dispatch to delivery, and no tracking,</li>"));
            Assert.IsTrue(htmlSource.Contains("<li>Express Airmail - approximately 5-10 working days from dispatch to delivery, and parcel tracking provided,</li>"));
            Assert.IsTrue(htmlSource.Contains("<li>UPS - approximately 2-5 working days from dispatch to delivery, and parcel tracking provided</li>"));
        }
    }
}