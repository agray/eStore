using NUnit.Framework;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.handlers; //HttpSimulator
using Subtext.TestLibrary;

namespace eStoreTests {
    [TestFixture]
    public class BasePageTests {
        private BasePage mBP;

        [SetUp]
        public void SetUp() { mBP = new BasePage(); }

        [TearDown]
        public void TearDown() { mBP = null; }

        [Test]
        public void setCurrencyByCountryTest() {
            using(new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                mBP.SetCurrencyByCountry(7); //Australia

                Assert.AreEqual("1", SessionHandler.Instance.Currency);
                Assert.AreEqual("AUD", SessionHandler.Instance.CurrencyValue);
                Assert.AreEqual(1, (int)SessionHandler.Instance.CurrencyXRate);
            }
        }
    }
}