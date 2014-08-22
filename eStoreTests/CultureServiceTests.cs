using com.phoenixconsulting.culture;
using NUnit.Framework;

//HttpSimulator

namespace eStoreTests {
    [TestFixture]
    public class CultureServiceTests {
        
        [SetUp]
        public void SetUp() {
        }

        [TearDown]
        public void TearDown() {
        }

        [Test]
        public void setCultureTest_AUD() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                CultureService.SetCulture("AUD");
                var currSymOut = CultureService.getCurrencySymbol();
                Assert.AreEqual("$", currSymOut);
            }
        }

        [Test]
        public void setCultureTest_USD() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                CultureService.SetCulture("USD");

                var currSymOut = CultureService.getCurrencySymbol();
                Assert.AreEqual("$", currSymOut);
            }
        }

        [Test]
        public void setCultureTest_EUR() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                CultureService.SetCulture("EUR");

                var currSymOut = CultureService.getCurrencySymbol();
                Assert.True('€'.CompareTo(currSymOut.ToCharArray(0, 1)[0]) == 0);
            }
        }

        [Test]
        public void setCultureTest_GBP() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                CultureService.SetCulture("GBP");

                var currSymOut = CultureService.getCurrencySymbol();
                Assert.True('£'.CompareTo(currSymOut.ToCharArray(0, 1)[0]) == 0);
            }
        }

        [Test]
        public void setCultureTest_DKK() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                CultureService.SetCulture("DKK");

                var currSymOut = CultureService.getCurrencySymbol();
                Assert.AreEqual("", currSymOut);
            }
        }

        [Test]
        public void setCultureTest_SEK() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                CultureService.SetCulture("SEK");

                var currSymOut = CultureService.getCurrencySymbol();
                Assert.AreEqual("", currSymOut);
            }
        }

        [Test]
        public void setCultureTest_HKD() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                CultureService.SetCulture("HKD");

                var currSymOut = CultureService.getCurrencySymbol();
                Assert.AreEqual("$", currSymOut);
            }
        }

        //[Test]
        //public void getCultureCodeTest {
        //    Assert.AreEqual("en-AU", CultureService.GetCultureCode("AUD"));
        //    Assert.AreEqual("en-US", CultureService.GetCultureCode("USD"));
        //    Assert.AreEqual("es-ES", CultureService.GetCultureCode("EUR"));
        //    Assert.AreEqual("en-CA", CultureService.GetCultureCode("CAD"));
        //    Assert.AreEqual("da-DK", CultureService.GetCultureCode("DKK"));
        //    Assert.AreEqual("zh-HK", CultureService.GetCultureCode("HKD"));
        //    Assert.AreEqual("ja-JP", CultureService.GetCultureCode("JPY"));
        //    Assert.AreEqual("en-NZ", CultureService.GetCultureCode("NZD"));
        //    Assert.AreEqual("zh-SG", CultureService.GetCultureCode("SGD"));
        //    Assert.AreEqual("sv-SE", CultureService.GetCultureCode("SEK"));
        //    Assert.AreEqual("en-GB", CultureService.GetCultureCode("GBP"));
        //    Assert.AreEqual("en-AU", CultureService.GetCultureCode("RUBBISH"));
        //}
    }
}