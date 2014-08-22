using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests {
    [TestFixture]
    public class CartTests {
        private ISelenium _selenium;
        private StringBuilder _verificationErrors;
        private TestUtilities _testUtil;

        [SetUp]
        public void SetupTest() {
            _selenium = new DefaultSelenium("localhost", 4444, "*firefox", "http://www.mywipstore.com.au/");
            //selenium = new DefaultSelenium("localhost", 4444, "*firefox", "http://www.google.com/");
            _selenium.Start();
            _verificationErrors = new StringBuilder();
            _testUtil = new TestUtilities();
        }

        [TearDown]
        public void TeardownTest() {
            try {
                _selenium.Stop();
                _testUtil = null;
            } catch(Exception) {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual(String.Empty, _verificationErrors.ToString());
        }

        [Test]
        public void AddOneItemTest() {
            _testUtil.OpenHomePage(_selenium);
            _testUtil.AddOneItemHelper(_selenium);

            var htmlSource = _selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("Brush From CatzRUs"));
            Assert.IsTrue(htmlSource.Contains("Size: Large"));
            Assert.IsTrue(htmlSource.Contains("Color: Yellow"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: center;\" valign=\"middle\">1</td>"));
            Assert.IsTrue(htmlSource.Contains("Price(<span>AUD</span>)"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_UnitPriceLabel\">$80.00</span>"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: right; height: 25px;\">$93.20</td>"));
            Assert.IsTrue(htmlSource.Contains("Total (Inc GST)"));
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "13.20", "AUD");
        }

        [Test]
        public void AddExactSameItemTwiceTest() {
            _testUtil.OpenHomePage(_selenium);
            _testUtil.AddOneItemHelper(_selenium);

            _selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_ItemImage");
            _testUtil.WaitForPageToLoad(_selenium, "5000");
            _testUtil.AddItemToCart(_selenium);

            var htmlSource = _selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("Brush From CatzRUs"));
            Assert.IsTrue(htmlSource.Contains("Size: Large"));
            Assert.IsTrue(htmlSource.Contains("Color: Yellow"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: center;\" valign=\"middle\">2</td>"));
            Assert.IsTrue(htmlSource.Contains("Price(<span>AUD</span>)"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_UnitPriceLabel\">$160.00</span>"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: right; height: 25px;\">$186.40</td>"));
            Assert.IsTrue(htmlSource.Contains("Total (Inc GST)"));
            _testUtil.AssertNavCartValues(_selenium, "$", "160.00", "26.40", "AUD");
        }

        [Test]
        public void AddSameItemDifferentAttributesTest() {
            _testUtil.OpenHomePage(_selenium);
            _testUtil.AddOneItemHelper(_selenium);
            _selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_ItemImage");
            _testUtil.WaitForPageToLoad(_selenium, "2000");
            _selenium.Select("ctl00_ContentPlaceHolder1_ProductFormView_productSizeDDL_SizeDropDownList", "label=Medium");
            _selenium.Select("ctl00_ContentPlaceHolder1_ProductFormView_productColorDDL_ColorDropDownList", "label=Red");
            _testUtil.AddItemToCart(_selenium);

            var htmlSource = _selenium.GetHtmlSource();
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
            _testUtil.AssertNavCartValues(_selenium, "$", "160.00", "26.40", "AUD");
        }

        [Test]
        public void AddOneRemoveOneItemTest() {
            _testUtil.OpenHomePage(_selenium);
            _testUtil.AddOneItemHelper(_selenium);
            _testUtil.RemoveFirstItemHelper(_selenium);
            Assert.IsTrue(_selenium.GetHtmlSource().Contains("Your Cart is Empty."));
        }

        [Test]
        public void AddExactSameItemTwiceThenRemoveTest() {
            _testUtil.OpenHomePage(_selenium);
            _testUtil.AddOneItemHelper(_selenium);
            _selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_ItemImage");
            _testUtil.WaitForPageToLoad(_selenium, "2000");
            _testUtil.AddItemToCart(_selenium);
            _testUtil.RemoveFirstItemHelper(_selenium);
            Assert.IsTrue(_selenium.GetHtmlSource().Contains("Your Cart is Empty."));
        }

        [Test]
        public void AddSameItemDifferentAttributesThenRemoveOneTest() {
            _testUtil.OpenHomePage(_selenium);
            _testUtil.AddOneItemHelper(_selenium);
            _selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_ItemImage");
            _testUtil.WaitForPageToLoad(_selenium, "2000");
            _selenium.Select("ctl00_ContentPlaceHolder1_ProductFormView_productSizeDDL_SizeDropDownList", "label=Small");
            _selenium.Select("ctl00_ContentPlaceHolder1_ProductFormView_productColorDDL_ColorDropDownList", "label=Orange");
            _selenium.Click("ctl00_ContentPlaceHolder1_ProductFormView_AddToCartImageButton");
            _testUtil.WaitForPageToLoad(_selenium, "2000");
            _testUtil.RemoveFirstItemHelper(_selenium);

            var htmlSource = _selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("Brush From CatzRUs"));
            Assert.IsTrue(htmlSource.Contains("<br>Size: Small"));
            Assert.IsTrue(htmlSource.Contains("<br>Color: Orange"));
            Assert.IsTrue(htmlSource.Contains("Price(<span>AUD</span>)"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_UnitPriceLabel\">$80.00</span>"));
            Assert.IsTrue(htmlSource.Contains("Ship 1.5 kg to"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: right; height: 25px;\">$93.20</td>"));
            Assert.IsTrue(htmlSource.Contains("Total (Inc GST)"));
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "13.20", "AUD");
        }

        [Test]
        public void calculateShippingCostsTest_NoWeight() {
            _testUtil.OpenHomePage(_selenium);
            _testUtil.AddWrappingItemHelper(_selenium);

            _testUtil.AssertViewCartTableFigures(_selenium, "$", "6.25", "0.00");
        }

        [Test]
        public void ChangeShippingCountryTest() {
            _testUtil.OpenHomePage(_selenium);
            _testUtil.WaitForPageToLoad(_selenium, "2000");
            _testUtil.AddOneItemHelper(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Austria", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[2]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            //Console.WriteLine(selenium.GetHtmlSource());
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Belgium", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[3]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Australia", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[1]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "13.20", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "93.20", "13.20");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Canada", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[4]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "36.55", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "116.55", "36.55");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Denmark", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[5]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Finland", "//option[@value='11']");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "France", "//option[@value='12']");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Germany", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[8]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Greece", "//option[@value='13']");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Hong Kong", "//option[@value='14']");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "30.25", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "110.25", "30.25");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Ireland", "//option[@value='15']");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Italy", "//option[@value='16']");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Japan", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[13]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "30.25", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "110.25", "30.25");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Netherlands", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[14]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "New Zealand", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[15]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "23.85", "AUD");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Singapore", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[16]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "30.25", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "110.25", "30.25");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Spain", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[17]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "Sweden", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[18]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "United Kingdom", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[19]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "46.05", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "126.05", "46.05");

            _testUtil.ChangeShippingCountryHelper(_selenium, "United States", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[20]");
            _testUtil.AssertNavCartValues(_selenium, "$", "80.00", "36.55", "AUD");
            _testUtil.AssertViewCartTableFigures(_selenium, "$", "116.55", "36.55");
        }

        [Test]
        public void ChangeShippingModeTest() {
            _testUtil.OpenHomePage(_selenium);
            _testUtil.WaitForPageToLoad(_selenium, "2000");
            _testUtil.AddOneItemHelper(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Austria", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[2]");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Belgium", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[3]");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Australia", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[1]");
            _testUtil.AssertZoneZ(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Canada", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[4]");
            _testUtil.AssertZoneC(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Denmark", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[5]");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Finland", "//option[@value='11']");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "France", "//option[@value='12']");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Germany", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[8]");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Greece", "//option[@value='13']");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Hong Kong", "//option[@value='14']");
            _testUtil.AssertZoneB(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Ireland", "//option[@value='15']");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Italy", "//option[@value='16']");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Japan", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[13]");
            _testUtil.AssertZoneB(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Netherlands", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[14]");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "New Zealand", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[15]");
            _testUtil.AssertZoneA(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Singapore", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[16]");
            _testUtil.AssertZoneB(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Spain", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[17]");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "Sweden", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[18]");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "United Kingdom", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[19]");
            _testUtil.AssertZoneD(_selenium);

            _testUtil.ChangeShippingCountryHelper(_selenium, "United States", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry']/option[20]");
            _testUtil.AssertZoneC(_selenium);
        }

        [Test]
        public void ChangeCurrencyTest() {
            _testUtil.OpenHomePage(_selenium);
            _testUtil.AddOneItemHelper(_selenium);

            _testUtil.ChangeCurrencyHelper(_selenium, "Hong Kong Dollars", "5");
            _testUtil.AssertViewCartLabels(_selenium, "$", "Total (Ex GST)", "HKD");

            _testUtil.ChangeCurrencyHelper(_selenium, "Australian Dollars", "1");
            _testUtil.AssertViewCartLabels(_selenium, "$", "Total (Inc GST)", "AUD");

            _testUtil.ChangeCurrencyHelper(_selenium, "Canadian Dollars", "2");
            _testUtil.AssertViewCartLabels(_selenium, "$", "Total (Ex GST)", "CAD");

            _testUtil.ChangeCurrencyHelper(_selenium, "Danish Kroners", "3");
            _testUtil.AssertViewCartLabels(_selenium, "", "Total (Ex GST)", "DKK");

            _testUtil.ChangeCurrencyHelper(_selenium, "Japanese Yen", "6");
            _testUtil.AssertViewCartLabels(_selenium, "¥", "Total (Ex GST)", "JPY");

            _testUtil.ChangeCurrencyHelper(_selenium, "NZ Dollars", "7");
            _testUtil.AssertViewCartLabels(_selenium, "$", "Total (Ex GST)", "NZD");

            _testUtil.ChangeCurrencyHelper(_selenium, "Singaporian Dollars", "8");
            _testUtil.AssertViewCartLabels(_selenium, "$", "Total (Ex GST)", "SGD");

            _testUtil.ChangeCurrencyHelper(_selenium, "Swedish Kroners", "9");
            _testUtil.AssertViewCartLabels(_selenium, "", "Total (Ex GST)", "SEK");

            _testUtil.ChangeCurrencyHelper(_selenium, "UK Pounds", "10");
            _testUtil.AssertViewCartLabels(_selenium, "£", "Total (Ex GST)", "GBP");

            _testUtil.ChangeCurrencyHelper(_selenium, "US Dollars", "30");
            _testUtil.AssertViewCartLabels(_selenium, "$", "Total (Ex GST)", "USD");

            _testUtil.ChangeCurrencyHelper(_selenium, "Euros", "31");
            _testUtil.AssertViewCartLabels(_selenium, "€", "Total (Ex GST)", "EUR");
        }

        [Test]
        public void ContinueShoppingTest() {
            _testUtil.OpenHomePage(_selenium);
            _testUtil.AddOneItemHelper(_selenium);

            _selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_ItemImage");
            _testUtil.WaitForPageToLoad(_selenium, "2000");
            Assert.IsTrue(_selenium.GetHtmlSource().Contains("Brush"));
        }

        [Test]
        public void GiftWrappedTest() {
            _testUtil.OpenHomePage(_selenium);
            _testUtil.AddOneItemHelper(_selenium);

            _selenium.Click("ctl00_ContentPlaceHolder1_GiftWrapHyperlink");
            _testUtil.WaitForPageToLoad(_selenium, "2000");
            Assert.IsTrue(_selenium.GetHtmlSource().Contains("Gift Wrapping"));
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
            _testUtil.OpenHomePage(_selenium);
            _testUtil.AddOneItemHelper(_selenium);

            _selenium.Click("link=FAQ");
            _testUtil.WaitForPageToLoad(_selenium, "3000");

            var htmlSource = _selenium.GetHtmlSource();
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