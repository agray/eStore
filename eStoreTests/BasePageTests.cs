using NUnit.Framework;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.handlers; //HttpSimulator

namespace eStoreTests {
    [TestFixture]
    public class BasePageTests {
        private BasePage _mBp;

        [SetUp]
        public void SetUp() { _mBp = new BasePage(); }

        [TearDown]
        public void TearDown() { _mBp = null; }

        [Test]
        public void SetCurrencyByCountryTest() {
            using(new HttpSimulator.HttpSimulator("/", "c:\\inetpub\\").SimulateRequest()) {
                _mBp.SetCurrencyByCountry(7); //Australia

                Assert.AreEqual("1", SessionHandler.Instance.Currency);
                Assert.AreEqual("AUD", SessionHandler.Instance.CurrencyValue);
                Assert.AreEqual(1, (int)SessionHandler.Instance.CurrencyXRate);
            }
        }
    }
}