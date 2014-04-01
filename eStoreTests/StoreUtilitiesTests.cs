using System.Threading;
using System.Globalization;
using NUnit.Framework;
using Subtext.TestLibrary; //HttpSimulator

using eStoreWeb;

namespace eStoreTests
{
    [TestFixture()]
    public class StoreUtilitiesTests
    {
        private StoreUtilities mU;

        [SetUp()]
        public void SetUp()
        {
            mU = new StoreUtilities();
        }

        [TearDown()]
        public void TearDown()
        {
            mU = null;
        }

        [Test()]
        public void setCurrencyByCountryTest()
        {
            using (new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest())
            {
                mU.setCurrencyByCountry(7); //Australia

                Assert.AreEqual("1", SessionHandler.Currency);
                Assert.AreEqual("AUD", SessionHandler.CurrencyValue);
                Assert.AreEqual(1, (int)SessionHandler.CurrencyXRate);
            }
        }

        [Test()]
        public void setCurrencyByIDTest_Australia()
        {
            using (new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest())
            {
                StoreUtilities.setCurrencyByID(1);
                //Australia

                Assert.AreEqual("1", SessionHandler.Currency);
                Assert.AreEqual("AUD", SessionHandler.CurrencyValue);
                Assert.AreEqual(1, (int)SessionHandler.CurrencyXRate);
            }
        }

        [Test()]
        public void setCurrencyByIDTest_Japan()
        {
            using (new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest())
            {
                StoreUtilities.setCurrencyByID(6);
                //Japan

                Assert.AreEqual("6", SessionHandler.Currency);
                Assert.AreEqual("JPY", SessionHandler.CurrencyValue);
                //Fudgy test
                Assert.Greater(90, SessionHandler.CurrencyXRate);
            }
        }

        [Test()]
        public void setCultureTest_AUD()
        {
            using (new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest())
            {
                string currSymOut = null;
                StoreUtilities.setCulture("AUD");

                currSymOut = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;
                Assert.AreEqual("$", currSymOut);
            }
        }

        [Test()]
        public void setCultureTest_USD()
        {
            using (new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest())
            {
                string currSymOut = null;
                StoreUtilities.setCulture("USD");

                currSymOut = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;
                Assert.AreEqual("$", currSymOut);
            }
        }

        [Test()]
        public void setCultureTest_EUR()
        {
            using (new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest())
            {
                string currSymOut = null;
                StoreUtilities.setCulture("EUR");

                currSymOut = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;
                Assert.True('€'.CompareTo(currSymOut.ToCharArray(0, 1)[0]) == 0);
            }
        }

        [Test()]
        public void setCultureTest_GBP()
        {
            using (new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest())
            {
                string currSymOut = null;
                StoreUtilities.setCulture("GBP");

                currSymOut = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;
                Assert.True('£'.CompareTo(currSymOut.ToCharArray(0, 1)[0]) == 0);
            }
        }

        [Test()]
        public void setCultureTest_DKK()
        {
            using (new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest())
            {
                string currSymOut = null;
                StoreUtilities.setCulture("DKK");

                currSymOut = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;
                Assert.AreEqual("", currSymOut);
            }
        }

        [Test()]
        public void setCultureTest_SEK()
        {
            using (new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest())
            {
                string currSymOut = null;
                StoreUtilities.setCulture("SEK");

                currSymOut = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;
                Assert.AreEqual("", currSymOut);
            }
        }

        [Test()]
        public void setCultureTest_HKD()
        {
            using (new HttpSimulator("/", "c:\\inetpub\\").SimulateRequest())
            {
                string currSymOut = null;
                StoreUtilities.setCulture("HKD");

                currSymOut = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;
                Assert.AreEqual("$", currSymOut);
            }
        }

        [Test()]
        public void getCultureCodeTest()
        {
            Assert.AreEqual("en-AU", StoreUtilities.getCultureCode("AUD"));
            Assert.AreEqual("en-US", StoreUtilities.getCultureCode("USD"));
            Assert.AreEqual("es-ES", StoreUtilities.getCultureCode("EUR"));
            Assert.AreEqual("en-CA", StoreUtilities.getCultureCode("CAD"));
            Assert.AreEqual("da-DK", StoreUtilities.getCultureCode("DKK"));
            Assert.AreEqual("zh-HK", StoreUtilities.getCultureCode("HKD"));
            Assert.AreEqual("ja-JP", StoreUtilities.getCultureCode("JPY"));
            Assert.AreEqual("en-NZ", StoreUtilities.getCultureCode("NZD"));
            Assert.AreEqual("zh-SG", StoreUtilities.getCultureCode("SGD"));
            Assert.AreEqual("sv-SE", StoreUtilities.getCultureCode("SEK"));
            Assert.AreEqual("en-GB", StoreUtilities.getCultureCode("GBP"));
            Assert.AreEqual("en-AU", StoreUtilities.getCultureCode("RUBBISH"));
        }
    }
}