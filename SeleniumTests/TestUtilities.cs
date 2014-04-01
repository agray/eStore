using NUnit.Framework;
using Selenium;

namespace SeleniumTests {
    public class TestUtilities {
        public void OpenHomePage(ISelenium selenium) {
            selenium.Open("/");
            WaitForPageToLoad(selenium, "5000");
        }

        public void ProductPageHelper(ISelenium selenium) {
            OpenHomePage(selenium);
            selenium.Click("ctl00_NavCatalogue_DepartmentRepeater_ctl00_CategoryRepeater_ctl00_CategoryHyperLink");
            WaitForPageToLoad(selenium, "5000");
            //selenium.Click("ctl00_ContentPlaceHolder1_CategoryList_ctrl0_ctl01_ProductImage");
            selenium.Click("ctl00_ContentPlaceHolder1_CategoryList_ctrl0_ctl01_ProductHyperLink");
            WaitForPageToLoad(selenium, "5000");
        }

        public void ChangeShippingCountryHelper(ISelenium selenium, string countryName, string clickString) {
            selenium.Select("ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipCountry", "label=" + countryName);
            selenium.Click(clickString);
            WaitForPageToLoad(selenium, "2000");

        }

        public void ChangeShippingModeHelper(ISelenium selenium, string modeName, string clickString) {
            selenium.Select("ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipMode", "label=" + modeName);
            selenium.Click(clickString);
            WaitForPageToLoad(selenium, "2000");
        }

        public void AddOneItemHelper(ISelenium selenium) {
            selenium.Click("ctl00_NavCatalogue_DepartmentRepeater_ctl00_CategoryRepeater_ctl00_CategoryHyperLink");
            selenium.WaitForPageToLoad("5000");
            selenium.Click("ctl00_ContentPlaceHolder1_CategoryList_ctrl0_ctl01_ProductImage");
            WaitForPageToLoad(selenium, "5000");
            AddItemToCart(selenium);
        }

        public void AddWrappingItemHelper(ISelenium selenium) {
            selenium.Click("ctl00_NavCatalogue_DepartmentRepeater_ctl04_CategoryRepeater_ctl00_CategoryHyperLink");
            selenium.WaitForPageToLoad("3000");
            selenium.Click("ctl00_ContentPlaceHolder1_CategoryList_ctrl0_ctl01_ProductImage");
            selenium.WaitForPageToLoad("3000");
            AddItemToCart(selenium);
            selenium.WaitForPageToLoad("3000");
        }

        public void AddItemToCart(ISelenium selenium) {
            selenium.Click("ctl00_ContentPlaceHolder1_ProductFormView_AddToCartImageButton");
            selenium.WaitForPageToLoad("30000");
        }

        public void ChangeCurrencyHelper(ISelenium selenium, string country, string option) {
            selenium.Select("ctl00_currencyDDL_CurrencyDropDownList", "label=" + country);
            selenium.Click("//option[@value='" + option + "']");
            WaitForPageToLoad(selenium, "4000");
        }

        public void RemoveFirstItemHelper(ISelenium selenium) {
            selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl00_DeleteLinkButton");
            WaitForPageToLoad(selenium, "2000");
        }

        public void RemoveSecondItemHelper(ISelenium selenium) {
            selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl01_DeleteLinkButton");
            WaitForPageToLoad(selenium, "2000");
        }

        public void RemoveThirdItemHelper(ISelenium selenium) {
            selenium.Click("ctl00_ContentPlaceHolder1_ShoppingCartTable_cartItemRepeater_ctl02_DeleteLinkButton");
            WaitForPageToLoad(selenium, "2000");
        }

        public void AssertNavCartValues(ISelenium selenium, string symbol, string total, string shipping, string currency) {
            //if(currency.Equals("EUR")) {
            //    Console.WriteLine("symbol: " + symbol);
            //}
            string htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_NavCart_ItemsTotalLabel\">" + currency + "&nbsp;" + symbol + total + "</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_NavCart_ShippingTotalLabel\">" + currency + "&nbsp;" + symbol + shipping + "</span>"));
        }

        public void AssertViewCartTableFigures(ISelenium selenium, string symbol, string total, string shipping) {
            //Console.WriteLine("<td style=\"text-align: right;\">" + symbol + shipping + "</td>");
            //Console.WriteLine("<td style=\"text-align: right; height: 25px;\">" + symbol + total + "</td>");
            string htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: right;\">" + symbol + shipping + "</td>"));
            Assert.IsTrue(htmlSource.Contains("<td style=\"text-align: right; height: 25px;\">" + symbol + total + "</td>"));
        }

        public void AssertViewCartLabels(ISelenium selenium, string symbol, string totalLabel, string priceHeading) {
            string htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(!symbol.Equals("") ? htmlSource.Contains(symbol) : htmlSource.Contains(","));
            Assert.IsTrue(htmlSource.Contains(totalLabel));
            Assert.IsTrue(htmlSource.Contains("Total Price(<span>" + priceHeading + "</span>)"));
        }

        public void AssertZoneA(ISelenium selenium) {
            ChangeShippingModeHelper(selenium, "Airmail", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipMode']/option[1]");
            AssertNavCartValues(selenium, "$", "80.00", "23.85", "AUD");
            AssertViewCartTableFigures(selenium, "$", "103.85", "23.85");
            ChangeShippingModeHelper(selenium, "Express Airmail", "//option[@value='4']");
            AssertNavCartValues(selenium, "$", "80.00", "35.25", "AUD");
            AssertViewCartTableFigures(selenium, "$", "115.25", "35.25");
            ChangeShippingModeHelper(selenium, "UPS", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipMode']/option[3]");
            AssertNavCartValues(selenium, "$", "80.00", "53.00", "AUD");
            AssertViewCartTableFigures(selenium, "$", "133.00", "53.00");
        }

        public void AssertZoneB(ISelenium selenium) {
            ChangeShippingModeHelper(selenium, "Airmail", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipMode']/option[1]");
            AssertNavCartValues(selenium, "$", "80.00", "30.25", "AUD");
            AssertViewCartTableFigures(selenium, "$", "110.25", "30.25");
            ChangeShippingModeHelper(selenium, "Express Airmail", "//option[@value='4']");
            AssertNavCartValues(selenium, "$", "80.00", "43.75", "AUD");
            AssertViewCartTableFigures(selenium, "$", "123.75", "43.75");
            ChangeShippingModeHelper(selenium, "UPS", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipMode']/option[3]");
            AssertNavCartValues(selenium, "$", "80.00", "59.00", "AUD");
            AssertViewCartTableFigures(selenium, "$", "139.00", "59.00");
        }

        public void AssertZoneC(ISelenium selenium) {
            ChangeShippingModeHelper(selenium, "Airmail", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipMode']/option[1]");
            AssertNavCartValues(selenium, "$", "80.00", "36.55", "AUD");
            AssertViewCartTableFigures(selenium, "$", "116.55", "36.55");
            ChangeShippingModeHelper(selenium, "Express Airmail", "//option[@value='4']");
            AssertNavCartValues(selenium, "$", "80.00", "52.15", "AUD");
            AssertViewCartTableFigures(selenium, "$", "132.15", "52.15");
            ChangeShippingModeHelper(selenium, "UPS", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipMode']/option[3]");
            AssertNavCartValues(selenium, "$", "80.00", "67.00", "AUD");
            AssertViewCartTableFigures(selenium, "$", "147.00", "67.00");
        }

        public void AssertZoneD(ISelenium selenium) {
            ChangeShippingModeHelper(selenium, "Airmail", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipMode']/option[1]");
            AssertNavCartValues(selenium, "$", "80.00", "46.05", "AUD");
            AssertViewCartTableFigures(selenium, "$", "126.05", "46.05");
            ChangeShippingModeHelper(selenium, "Express Airmail", "//option[@value='4']");
            AssertNavCartValues(selenium, "$", "80.00", "64.85", "AUD");
            AssertViewCartTableFigures(selenium, "$", "144.85", "64.85");
            ChangeShippingModeHelper(selenium, "UPS", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipMode']/option[3]");
            AssertNavCartValues(selenium, "$", "80.00", "74.00", "AUD");
            AssertViewCartTableFigures(selenium, "$", "154.00", "74.00");
        }

        public void AssertZoneZ(ISelenium selenium) {
            ChangeShippingModeHelper(selenium, "Airmail", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipMode']/option[1]");
            AssertNavCartValues(selenium, "$", "80.00", "13.20", "AUD");
            AssertViewCartTableFigures(selenium, "$", "93.20", "13.20");
            ChangeShippingModeHelper(selenium, "Express Airmail", "//option[@value='4']");
            AssertNavCartValues(selenium, "$", "80.00", "13.20", "AUD");
            AssertViewCartTableFigures(selenium, "$", "93.20", "13.20");
            ChangeShippingModeHelper(selenium, "UPS", "//select[@id='ctl00_ContentPlaceHolder1_ShoppingCartTable_CountryAndModeDDLs_cartShipMode']/option[3]");
            AssertNavCartValues(selenium, "$", "80.00", "13.20", "AUD");
            AssertViewCartTableFigures(selenium, "$", "93.20", "13.20");
        }

        public void AssertDepartments(ISelenium selenium, string currency, string symbol) {
            string htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("<span class=\"CategoryName\">Grooming</span>"));
            Assert.IsTrue(htmlSource.Contains("<span class=\"CategoryName\">BathTime</span>"));
            //Assert.IsTrue(htmlSource.Contains("<span class=\"CategoryName\">Snakes</span>"));
            //Assert.IsTrue(htmlSource.Contains("<span class=\"CategoryName\">Possums</span>"));
            //Assert.IsTrue(htmlSource.Contains("<span class=\"CategoryName\">spiders</span>"));
            Assert.IsTrue(htmlSource.Contains("from " + currency + " " + symbol));
        }

        public void EnterCheckOutPages(ISelenium selenium) {
            selenium.Click("ctl00_ContentPlaceHolder1_CheckOutImageButton");
            WaitForPageToLoad(selenium, "9000");
        }

        public void WaitForPageToLoad(ISelenium selenium, string millis) {
            try {
                selenium.WaitForPageToLoad(millis);
            } catch(SeleniumException) {}
        }

        public bool SelectValueEqual(ISelenium selenium, string selector, string value) {
            return selenium.GetSelectedLabel(selector) == value;
        }

        public bool TextValueEqual(ISelenium selenium, string selector, string value) {
            return selenium.GetValue(selector) == value;
        }

        public void EnterCheckOut1Data(ISelenium selenium) {
            selenium.Type("ctl00_ContentPlaceHolder1_EmailTextBox", "csharp@mail.com");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerFirstNameTextBox", "Andrew");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerLastNameTextBox", "Gray");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerAddressTextBox", "1258 Toorak Road");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerSuburbTextBox", "Camberwell");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerStateTextBox", "VIC");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerPostcodeTextBox", "3124");
            //selenium.Type("ctl00_ContentPlaceHolder1_CaptchaTextBox", "99999");
        }

        public void EnterInvalidCheckOut1Data(ISelenium selenium) {
            selenium.Type("ctl00_ContentPlaceHolder1_EmailTextBox", "csharp@com");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerFirstNameTextBox", "Andrew");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerLastNameTextBox", "Gray");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerAddressTextBox", "1258 Toorak Road");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerSuburbTextBox", "Camberwell");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerStateTextBox", "VIC");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerPostcodeTextBox", "3124");
        }

        public void EnterCheckOut1DataAndSubmit(ISelenium selenium) {
            selenium.Type("ctl00_ContentPlaceHolder1_EmailTextBox", "csharp@mail.com");
            //Console.WriteLine("START");
            //Console.WriteLine(selenium.GetHtmlSource());
            //Console.WriteLine("END");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerFirstNameTextBox", "Andrew");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerLastNameTextBox", "Gray");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerAddressTextBox", "1258 Toorak Road");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerSuburbTextBox", "Camberwell");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerStateTextBox", "VIC");
            selenium.Type("ctl00_ContentPlaceHolder1_CustomerPostcodeTextBox", "3124");
            //selenium.Type("ctl00_ContentPlaceHolder1_CaptchaTextBox", "99999");
            selenium.Click("ctl00_ContentPlaceHolder1_NextButton");
            WaitForPageToLoad(selenium, "6000");
        }

        public void EnterCheckOut2Data(ISelenium selenium) {
            selenium.Select("ctl00_ContentPlaceHolder1_sameAddressDDL_YesNoDropDownList", "label=No");
            WaitForPageToLoad(selenium, "6000");
            selenium.Type("ctl00_ContentPlaceHolder1_ShippingFirstNameTextBox", "Felicity");
            selenium.Type("ctl00_ContentPlaceHolder1_ShippingLastNameTextBox", "Davies");
            selenium.Type("ctl00_ContentPlaceHolder1_ShippingAddressTextBox", "10 Smith Street");
            selenium.Type("ctl00_ContentPlaceHolder1_ShippingSuburbTextBox", "SmithTown");
            selenium.Type("ctl00_ContentPlaceHolder1_ShippingStateTextBox", "NSW");
            selenium.Type("ctl00_ContentPlaceHolder1_ShippingPostcodeTextBox", "2000");
            selenium.Select("ctl00_ContentPlaceHolder1_countryDDL_CountryDropDownList", "label=Hong Kong");
        }

        public void EnterCheckOut2DataAndSubmit(ISelenium selenium) {
            selenium.Select("ctl00_ContentPlaceHolder1_sameAddressDDL_YesNoDropDownList", "label=No");
            WaitForPageToLoad(selenium, "10000");
            selenium.Type("ctl00_ContentPlaceHolder1_ShippingFirstNameTextBox", "Felicity");
            selenium.Type("ctl00_ContentPlaceHolder1_ShippingLastNameTextBox", "Davies");
            selenium.Type("ctl00_ContentPlaceHolder1_ShippingAddressTextBox", "10 Smith Street");
            selenium.Type("ctl00_ContentPlaceHolder1_ShippingSuburbTextBox", "SmithTown");
            selenium.Type("ctl00_ContentPlaceHolder1_ShippingStateTextBox", "NSW");
            selenium.Type("ctl00_ContentPlaceHolder1_ShippingPostcodeTextBox", "2000");
            WaitForPageToLoad(selenium, "10000");
            selenium.Select("ctl00_ContentPlaceHolder1_countryDDL_CountryDropDownList", "label=Hong Kong");
            WaitForPageToLoad(selenium, "10000");
            selenium.Click("ctl00_ContentPlaceHolder1_NextButton");
            WaitForPageToLoad(selenium, "10000");
        }

        public void EnterCheckOut3Data(ISelenium selenium) {
            WaitForPageToLoad(selenium, "10000");
            selenium.Type("ctl00_ContentPlaceHolder1_CardholderTextBox", "Andre Valleta");
            selenium.Select("ctl00_ContentPlaceHolder1_cardTypeDDL_CardTypeDropDownList", "label=Visa");
            selenium.Type("ctl00_ContentPlaceHolder1_CardNumberTextBox", "12344");
            selenium.Type("ctl00_ContentPlaceHolder1_CardValidationValueTextBox", "1234");
            selenium.Select("ctl00_ContentPlaceHolder1_ExpiryMonthDDL_ExpiryMonthDropDownList", "label=02");
            WaitForPageToLoad(selenium, "6000");
            selenium.Select("ctl00_ContentPlaceHolder1_ExpiryYearDDL_ExpiryYearDropDownList", "label=2011");
            WaitForPageToLoad(selenium, "6000");
        }

        public void EnterCheckOut3DataAndSubmit(ISelenium selenium) {
            selenium.Type("ctl00_ContentPlaceHolder1_CardholderTextBox", "Andre Valleta");
            selenium.Select("ctl00_ContentPlaceHolder1_cardTypeDDL_CardTypeDropDownList", "label=Visa");
            selenium.Type("ctl00_ContentPlaceHolder1_CardNumberTextBox", "12344");
            selenium.Type("ctl00_ContentPlaceHolder1_CardValidationValueTextBox", "1234");
            selenium.Select("ctl00_ContentPlaceHolder1_ExpiryMonthDDL_ExpiryMonthDropDownList", "label=02");
            selenium.Select("ctl00_ContentPlaceHolder1_ExpiryYearDDL_ExpiryYearDropDownList", "label=2011");
            selenium.Click("ctl00_ContentPlaceHolder1_NextButton");
            WaitForPageToLoad(selenium, "10000");
        }

        public void EnterDifferentCheckOut3DataAndSubmit(ISelenium selenium) {
            selenium.Type("ctl00_ContentPlaceHolder1_CardholderTextBox", "Bill Dodds");
            selenium.Select("ctl00_ContentPlaceHolder1_cardTypeDDL_CardTypeDropDownList", "label=MasterCard");
            selenium.Type("ctl00_ContentPlaceHolder1_CardNumberTextBox", "4444333322221111");
            selenium.Type("ctl00_ContentPlaceHolder1_CardValidationValueTextBox", "987");
            selenium.Select("ctl00_ContentPlaceHolder1_ExpiryMonthDDL_ExpiryMonthDropDownList", "label=01");
            selenium.Select("ctl00_ContentPlaceHolder1_ExpiryYearDDL_ExpiryYearDropDownList", "label=2010");
            selenium.Click("ctl00_ContentPlaceHolder1_NextButton");
            WaitForPageToLoad(selenium, "6000");
        }


        public void CurrencySetFromBillingCountryHelper(ISelenium selenium, string country, string currency) {
            selenium.Select("ctl00_ContentPlaceHolder1_CustomerCountryCodeDropDownList", "label=" + country);
            EnterCheckOut1DataAndSubmit(selenium);
            selenium.Click("ctl00_ContentPlaceHolder1_BackButton");
            WaitForPageToLoad(selenium, "6000");
            selenium.Click("ctl00_ContentPlaceHolder1_BackButton");
            WaitForPageToLoad(selenium, "6000");
            Assert.IsTrue(SelectValueEqual(selenium, "ctl00_currencyDDL_CurrencyDropDownList", currency));
        }

        public void AssertCheckout1RequiredFieldRedLabels(ISelenium selenium, string display) {
            string htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CustomerFirstNameTextBoxRFV\" style=\"color: Red; display: " + display + ";\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CustomerLastNameTextBoxRFV\" style=\"color: Red; display: " + display + ";\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CustomerAddressTextBoxRFV\" style=\"color: Red; display: " + display + ";\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CustomerSuburbTextBoxRFV\" style=\"color: Red; display: " + display + ";\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CustomerStateTextBoxRFV\" style=\"color: Red; display: " + display + ";\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CustomerPostcodeTextBoxRFV\" style=\"color: Red; display: " + display + ";\">Required Field</span>"));
        }

        public void AssertCheckout2RequiredFieldRedLabels(ISelenium selenium, string display) {
            string htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShippingFirstNameTextBoxRFV\" style=\"color: Red; display: " + display + ";\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShippingLastNameTextBoxRFV\" style=\"color: Red; display: " + display + ";\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShippingAddressTextBoxRFV\" style=\"color: Red; display: " + display + ";\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShippingSuburbTextBoxRFV\" style=\"color: Red; display: " + display + ";\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShippingStateTextBoxRFV\" style=\"color: Red; display: " + display + ";\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ShippingPostcodeTextBoxRFV\" style=\"color: Red; display: " + display + ";\">Required Field</span>"));
        }

        public void AssertCheckout3RequiredFieldRedLabels(ISelenium selenium) {
            string htmlSource = selenium.GetHtmlSource();
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CardholderTextBoxRequiredFieldValidator\" style=\"color: Red; display: inline;\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CardTypeDropDownListRequiredFieldValidator\" style=\"color: Red; display: inline;\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CardNumberTextBoxRequiredFieldValidator\" style=\"color: Red; display: inline;\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_CardValidationValueTextBoxRequiredFieldValidator\" style=\"color: Red; display: inline;\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ExpiryMonthDDLRequiredFieldValidator\" style=\"color: Red; display: inline;\">Required Field</span>"));
            Assert.IsTrue(htmlSource.Contains("<span id=\"ctl00_ContentPlaceHolder1_ExpiryYearDDLRequiredFieldValidator\" style=\"color: Red; display: inline;\">Required Field</span>"));
        }

        public bool IsDisabled(ISelenium selenium, string field) {
            return selenium.GetAttribute(field + "@disabled").Equals("disabled");
        }

        public bool IsEnabled(ISelenium selenium, string field) {
            return selenium.IsEditable(field);
        }


        public void MoveToPopUp(ISelenium selenium, string timeout) {
            try {
                selenium.WaitForPopUp(selenium.GetAllWindowTitles()[1], timeout);
            } catch(SeleniumException) {}
            selenium.SelectWindow(selenium.GetAllWindowTitles()[1]);
        }
    }
}