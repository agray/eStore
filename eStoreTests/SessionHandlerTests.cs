using System.Collections;
using System.Data;
using NUnit.Framework;
using phoenixconsulting.businessentities.list; //DTItem
using phoenixconsulting.common.handlers;
using Subtext.TestLibrary;


namespace eStoreTests {
    [TestFixture]
    public class SessionHandlerTests {
        [SetUp]
        public void SetUp() {
            //nothing to go here
        }

        [TearDown]
        public void TearDown() {
            //nothing to go here
        }

        [Test]
        public void CanGetSetShoppingCart() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                var cartItemIn = new DTItem(1, 3, 10, "Product for CanGetSetShoppingCart Unit Test", "Images\\300.gif", 100, 10, 1, 0, 100,
                                               10, "Red", 10, "Large");
                var listIn = new ArrayList();
                listIn.Add(cartItemIn);

                SessionHandler.Instance.ShoppingCart = listIn;
                var listOut = SessionHandler.Instance.ShoppingCart;
                var cartItemOut = (DTItem)listOut[0];

                Assert.AreEqual("Product for CanGetSetShoppingCart Unit Test", cartItemOut.ProductDetails, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPreviousPage() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.PreviousPage = "2";
                Assert.AreEqual("2", SessionHandler.Instance.PreviousPage, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCurrentPage() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CurrentPage = "2";
                Assert.AreEqual("2", SessionHandler.Instance.CurrentPage, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetDepID() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.DepartmentId = 2;
                Assert.AreEqual(2, SessionHandler.Instance.DepartmentId, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCatID() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CategoryId = 2;
                Assert.AreEqual(2, SessionHandler.Instance.CategoryId, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetProdID() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.ProductId = 2;
                Assert.AreEqual(2, SessionHandler.Instance.ProductId, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBrowsePageIndex() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BrowsePageIndex = "2";
                Assert.AreEqual("2", SessionHandler.Instance.BrowsePageIndex, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetAlsoBought() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.AlsoBought = true;
                Assert.AreEqual(true, SessionHandler.Instance.AlsoBought, "Was not able to retrieve session variable.");
                SessionHandler.Instance.AlsoBought = false;
                Assert.AreEqual(false, SessionHandler.Instance.AlsoBought, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetSearchString() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.SearchString = "ProductName";
                Assert.AreEqual("ProductName", SessionHandler.Instance.SearchString, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCurrencyXRate() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CurrencyXRate = 2.123;
                Assert.AreEqual(2.123, SessionHandler.Instance.CurrencyXRate, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCurrency() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.Currency = "2";
                Assert.AreEqual("2", SessionHandler.Instance.Currency, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCurrencyValue() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CurrencyValue = "AUD";
                Assert.AreEqual("AUD", SessionHandler.Instance.CurrencyValue, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetShipMode() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.ShippingMode = "9";
                Assert.AreEqual("9", SessionHandler.Instance.ShippingMode, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetShippingCountry() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.ShippingCountry = "1";
                Assert.AreEqual("1", SessionHandler.Instance.ShippingCountry, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetTotalCost() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.TotalCost = 100.99;
                Assert.AreEqual(100.99, SessionHandler.Instance.TotalCost, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetTotalWeight() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.TotalWeight = 6.5;
                Assert.AreEqual(6.5, SessionHandler.Instance.TotalWeight, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetTotalShipping() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.TotalShipping = 65.5;
                Assert.AreEqual(65.5, SessionHandler.Instance.TotalShipping, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBillingDetailsEmailAddress() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BillingEmailAddress = "bob@smithcorp.com.au";
                Assert.AreEqual("bob@smithcorp.com.au", SessionHandler.Instance.BillingEmailAddress, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBillingDetailsFirstName() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BillingFirstName = "Bob";
                Assert.AreEqual("Bob", SessionHandler.Instance.BillingFirstName, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBillingDetailsLastName() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BillingLastName = "Smith";
                Assert.AreEqual("Smith", SessionHandler.Instance.BillingLastName, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBillingDetailsAddress() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BillingAddress = "10 Charles Street";
                Assert.AreEqual("10 Charles Street", SessionHandler.Instance.BillingAddress, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBillingDetailsCitySuburb() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BillingCitySuburb = "St Kilda";
                Assert.AreEqual("St Kilda", SessionHandler.Instance.BillingCitySuburb, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBillingDetailsStateRegion() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BillingStateRegion = "VIC";
                Assert.AreEqual("VIC", SessionHandler.Instance.BillingStateRegion, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBillingDetailsPostCode() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BillingPostcode = "3000";
                Assert.AreEqual("3000", SessionHandler.Instance.BillingPostcode, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBillingDetailsCountry() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BillingCountry = "1";
                Assert.AreEqual("1", SessionHandler.Instance.BillingCountry, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBillingDetailsCountryCode() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BillingCountryCode = "AUS";
                Assert.AreEqual("AUS", SessionHandler.Instance.BillingCountryCode, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBillingCurrencyId() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BillingCurrencyId = "1";
                Assert.AreEqual("1", SessionHandler.Instance.BillingCurrencyId, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBillingCountryCurrencyValue() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BillingCurrencyValue = "JPY";
                Assert.AreEqual("JPY", SessionHandler.Instance.BillingCurrencyValue, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetBillingCountryXRate() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.BillingXRate = 3.02;
                Assert.AreEqual(3.02, SessionHandler.Instance.BillingXRate, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetSameAddress() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.SameAddress = "1";
                Assert.AreEqual("1", SessionHandler.Instance.SameAddress, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetShippingDetailsFirstName() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.ShippingFirstName = "Bob";
                Assert.AreEqual("Bob", SessionHandler.Instance.ShippingFirstName, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetShippingDetailsLastName() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.ShippingLastName = "Smith";
                Assert.AreEqual("Smith", SessionHandler.Instance.ShippingLastName, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetShippingDetailsAddress() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.ShippingAddress = "10 Charles Street";
                Assert.AreEqual("10 Charles Street", SessionHandler.Instance.ShippingAddress, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetShippingDetailsCitySuburb() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.ShippingCitySuburb = "St Kilda";
                Assert.AreEqual("St Kilda", SessionHandler.Instance.ShippingCitySuburb, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetShippingDetailsStateRegion() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.ShippingStateRegion = "VIC";
                Assert.AreEqual("VIC", SessionHandler.Instance.ShippingStateRegion, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetShippingDetailsPostCode() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.ShippingPostcode = "3000";
                Assert.AreEqual("3000", SessionHandler.Instance.ShippingPostcode, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetShippingDetailsCountry() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.ShippingCountry = "1";
                Assert.AreEqual("1", SessionHandler.Instance.ShippingCountry, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetShippingDetailsCountryCode() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.ShippingCountryCode = "AUS";
                Assert.AreEqual("AUS", SessionHandler.Instance.ShippingCountryCode, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetShippingDetailsCountryName() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.ShippingCountryName = "Hong Kong";
                Assert.AreEqual("Hong Kong", SessionHandler.Instance.ShippingCountryName, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPaymentType() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.PaymentType = "3";
                Assert.AreEqual("3", SessionHandler.Instance.PaymentType, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCardHolderName() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CardholderName = "Bob Smith";
                Assert.AreEqual("Bob Smith", SessionHandler.Instance.CardholderName, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCardNumber() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CardNumber = "1234567890123456";
                Assert.AreEqual("1234567890123456", SessionHandler.Instance.CardNumber, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCardType() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CardType = "1";
                Assert.AreEqual("1", SessionHandler.Instance.CardType, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCVV() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CVV = "1234";
                Assert.AreEqual("1234", SessionHandler.Instance.CVV, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCCExpiryMonth() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CCExpiryMonth = "1";
                Assert.AreEqual("1", SessionHandler.Instance.CCExpiryMonth, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCCExpiryYear() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CCExpiryYear = "16";
                Assert.AreEqual("16", SessionHandler.Instance.CCExpiryYear, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCCExpiryMonthItem() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CCExpiryMonthItem = "01";
                Assert.AreEqual("01", SessionHandler.Instance.CCExpiryMonthItem, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCCExpiryYearItem() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CCExpiryYearItem = "2013";
                Assert.AreEqual("2013", SessionHandler.Instance.CCExpiryYearItem, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetWrappingToTrue() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.Wrapping = true;
                Assert.AreEqual(true, SessionHandler.Instance.Wrapping, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetWrappingToFalse() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.Wrapping = false;
                Assert.AreEqual(false, SessionHandler.Instance.Wrapping, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetComment() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.Comment = "This is a test Comment";
                Assert.AreEqual("This is a test Comment", SessionHandler.Instance.Comment, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetGiftTag() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.GiftTag = "To my lovely wife";
                Assert.AreEqual("To my lovely wife", SessionHandler.Instance.GiftTag, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPreAuthID() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.PreAuthId = "4321";
                Assert.AreEqual("4321", SessionHandler.Instance.PreAuthId, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetTxnID() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.TxnId = "1234";
                Assert.AreEqual("1234", SessionHandler.Instance.TxnId, "Was not able to retrieve session variable.");
            }
        }

        //****************
        [Test]
        public void CanGetSetRequestXML() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CCRequestXml = "<test>This is a value 123</test>";
                Assert.AreEqual("<test>This is a value 123</test>", SessionHandler.Instance.CCRequestXml, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetResponseXML() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.CCResponseXml = "<test>This is a value 123</test>";
                Assert.AreEqual("<test>This is a value 123</test>", SessionHandler.Instance.CCResponseXml, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPaymentResponseText() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.PaymentResponseText = "Approved";
                Assert.AreEqual("Approved", SessionHandler.Instance.PaymentResponseText, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPaymentResponseCode() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.PaymentResponseCode = "00";
                Assert.AreEqual("00", SessionHandler.Instance.PaymentResponseCode, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetIsPaymentApproved() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.IsPaymentApproved = "Yes";
                Assert.AreEqual("Yes", SessionHandler.Instance.IsPaymentApproved, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetURL() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                SessionHandler.Instance.Url = "https://www.securepay.com.au/xmlapi/payment";
                Assert.AreEqual("https://www.securepay.com.au/xmlapi/payment", SessionHandler.Instance.Url, "Was not able to retrieve session variable.");
            }
        }


        //****************

        [Test]
        public void CanGetSetCartDataSet() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                var dsIn = new DataSet();
                var dtIn = new DataTable();
                var dsOut = new DataSet();
                var dtOut = new DataTable();
                dtIn.ExtendedProperties.Add("testKey23", "value23");
                dsIn.DataSetName = "Test";
                dsIn.Tables.Add(dtIn);
                SessionHandler.Instance.CartDataSet = dsIn;

                dsOut = SessionHandler.Instance.CartDataSet;
                Assert.AreEqual("Test", dsOut.DataSetName, "Was not able to retrieve dataSetName.");

                dtOut = dsOut.Tables[0];

                Assert.AreEqual("value23", dtOut.ExtendedProperties["testKey23"], "Was not able to retrieve ExtendedProperty.");
            }
        }
    }
}