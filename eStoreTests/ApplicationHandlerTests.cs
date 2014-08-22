using phoenixconsulting.common.handlers;
using NUnit.Framework;

//HttpSimulator

namespace eStoreTests {
    [TestFixture]
    public class ApplicationHandlerTests {
        [SetUp]
        public void SetUp() {
            //nothing to go here
        }

        [TearDown]
        public void TearDown() {
            //nothing to go here
        }

        [Test]
        public void CanGetSetTradingName() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.TradingName = "PetsParadise";
                Assert.AreEqual("PetsParadise", ApplicationHandler.Instance.TradingName, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetMerchantId() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.CCProcessorMerchantID = "ACBFDF123";
                Assert.AreEqual("ACBFDF123", ApplicationHandler.Instance.CCProcessorMerchantID, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPassword() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.CCProcessorPassword = "PetsParadise";
                Assert.AreEqual("PetsParadise", ApplicationHandler.Instance.CCProcessorPassword, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPaymentType() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.CCProcessorPaymentType = 0;
                Assert.AreEqual(0, ApplicationHandler.Instance.CCProcessorPaymentType, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetLiveEchoUrl() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.CCProcessorLiveEchoURL = "https://theurl.com";
                Assert.AreEqual("https://theurl.com", ApplicationHandler.Instance.CCProcessorLiveEchoURL, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetTestEchoUrl() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.CCProcessorTestEchoURL = "https://theurl.com";
                Assert.AreEqual("https://theurl.com", ApplicationHandler.Instance.CCProcessorTestEchoURL, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetLivePaymentUrl() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.CCProcessorLivePaymentURL = "https://theurl.com";
                Assert.AreEqual("https://theurl.com", ApplicationHandler.Instance.CCProcessorLivePaymentURL, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetTestPaymentUrl() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.CCProcessorTestPaymentURL = "https://theurl.com";
                Assert.AreEqual("https://theurl.com", ApplicationHandler.Instance.CCProcessorTestPaymentURL, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetLivePaymentDcurl() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.CCProcessorLivePaymentDCURL = "https://theurl.com";
                Assert.AreEqual("https://theurl.com", ApplicationHandler.Instance.CCProcessorLivePaymentDCURL, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetTestPaymentDcurl() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.CCProcessorTestPaymentDCURL = "https://theurl.com";
                Assert.AreEqual("https://theurl.com", ApplicationHandler.Instance.CCProcessorTestPaymentDCURL, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetRefundAdminFeePercent() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.RefundAdminFeePercent = 10;
                Assert.AreEqual(10, ApplicationHandler.Instance.RefundAdminFeePercent, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetCancelAdminFeePercent() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.CancelAdminFeePercent = 5;
                Assert.AreEqual(5, ApplicationHandler.Instance.CancelAdminFeePercent, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetSmtpServer() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.SMTPServer = "mail.iinet.net.au";
                Assert.AreEqual("mail.iinet.net.au", ApplicationHandler.Instance.SMTPServer, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetHomePageUrl() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.HomePageURL = "www.petsplayground.com.au";
                Assert.AreEqual("www.petsplayground.com.au", ApplicationHandler.Instance.HomePageURL, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetSupportEmail() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.SupportEmail = "andrew@here.com";
                Assert.AreEqual("andrew@here.com", ApplicationHandler.Instance.SupportEmail, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPaypalVersion() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.PaypalVersion = "57.0";
                Assert.AreEqual("57.0", ApplicationHandler.Instance.PaypalVersion, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPaypalApiUsername() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.PaypalAPIUsername = "andrew_1277537043_biz_api1.gmail.com";
                Assert.AreEqual("andrew_1277537043_biz_api1.gmail.com", ApplicationHandler.Instance.PaypalAPIUsername, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPaypalApiPassword() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.PaypalAPIPassword = "1277537047";
                Assert.AreEqual("1277537047", ApplicationHandler.Instance.PaypalAPIPassword, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPaypalApiSignature() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.PaypalAPISignature = "ALWIEvyUZP-qwxIHhgjyU0DwOnYmA0HtvHEQ9YZhj-OgFow0h3-.tZ9t";
                Assert.AreEqual("ALWIEvyUZP-qwxIHhgjyU0DwOnYmA0HtvHEQ9YZhj-OgFow0h3-.tZ9t", ApplicationHandler.Instance.PaypalAPISignature, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPaypalApiurl() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.PaypalAPIURL = "https://api-3t.sandbox.paypal.com/2.0/";
                Assert.AreEqual("https://api-3t.sandbox.paypal.com/2.0/", ApplicationHandler.Instance.PaypalAPIURL, "Was not able to retrieve session variable.");
            }
        }

        [Test]
        public void CanGetSetPaypalUrl() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                ApplicationHandler.Instance.PaypalURL = "http://www.paypal.com";
                Assert.AreEqual("http://www.paypal.com", ApplicationHandler.Instance.PaypalURL, "Was not able to retrieve session variable.");
            }
        }
    }
}