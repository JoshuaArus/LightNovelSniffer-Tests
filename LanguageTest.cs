using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using LightNovelSniffer.Config;
using LightNovelSniffer.Exception;
using LightNovelSniffer.Resources;
using LightNovelSniffer.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightNovelSniffer_Tests
{
    /// <summary>
    /// Summary description for LanguageTest
    /// </summary>
    [TestClass]
    public class LanguageTest
    {
        [TestMethod]
        public void TestDefaultLanguage()
        {
            ConfigTools.InitConf("Config.xml");
            CultureInfo ci = ConfigTools.GetCurrentLanguage();
            Assert.AreEqual(ci, CultureInfo.CurrentCulture);
        }

        [TestMethod]
        public void TestDefaultLanguageChange()
        {
            CultureInfo ci = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpper() == "FR" ? CultureInfo.GetCultureInfo("en") : CultureInfo.GetCultureInfo("de");

            ConfigTools.InitConf("Config.xml", ci);
            Assert.AreEqual(ci, ConfigTools.GetCurrentLanguage());
        }

        [TestMethod]
        [ExpectedException(typeof(CoverException))]
        public void TestDefaultLanguageMessages()
        {
            ResourceManager rm = new ResourceManager(typeof(LightNovelSniffer_Strings));
            string defaultTemplate = rm.GetString("CoverDownloadExceptionMessage");

            string url = "coucou";
            ConfigTools.InitConf("Config.xml");
            try
            {
                WebCrawler.DownloadCover(url);
            }
            catch (CoverException e)
            {
                Console.WriteLine(e.Message);

                string localizedTemplate = rm.GetString("CoverDownloadExceptionMessage");
                Assert.AreEqual(e.Message, string.Format(defaultTemplate, url));
                Assert.AreEqual(e.Message, string.Format(localizedTemplate, url));
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CoverException))]
        public void TestSpecifiedLanguageMessages()
        {
            ResourceManager rm = new ResourceManager(typeof(LightNovelSniffer_Strings));
            string defaultTemplate = rm.GetString("CoverDownloadExceptionMessage");

            string url = "coucou";
            CultureInfo ci = CultureInfo.GetCultureInfo("de");
            ConfigTools.InitConf("Config.xml", ci);
            try
            {
                WebCrawler.DownloadCover(url);
            }
            catch (CoverException e)
            {
                Console.WriteLine(e.Message);

                string localizedTemplate = rm.GetString("CoverDownloadExceptionMessage");
                Assert.AreEqual(e.Message, string.Format(localizedTemplate, url));
                Assert.AreNotEqual(e.Message, string.Format(defaultTemplate, url));
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CoverException))]
        public void TestExternalLanguageMessages()
        {
            ResourceManager rm = new ResourceManager(typeof(LightNovelSniffer_Strings));
            string defaultTemplate = rm.GetString("CoverDownloadExceptionMessage");

            string url = "coucou";
            CultureInfo ci = CultureInfo.GetCultureInfo("de");
            CultureInfo.DefaultThreadCurrentUICulture = ci;
            ConfigTools.InitConf("Config.xml");
            try
            {
                WebCrawler.DownloadCover(url);
            }
            catch (CoverException e)
            {
                Console.WriteLine(e.Message);
                string localizedTemplate = rm.GetString("CoverDownloadExceptionMessage");
                Assert.AreEqual(e.Message, string.Format(localizedTemplate, url));
                Assert.AreNotEqual(e.Message, string.Format(defaultTemplate, url));
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(LanguageException))]
        public void TestEmptyLanguageString()
        {
            ConfigTools.InitConf("Config.xml");
            try
            {
                ConfigTools.SetLanguage("");
            }
            catch (LanguageException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(LanguageException))]
        public void TestNullLanguageString()
        {
            ConfigTools.InitConf("Config.xml");
            try
            {
                ConfigTools.SetLanguage((string)null);
            }
            catch (LanguageException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(LanguageException))]
        public void TestInconsistentLanguageString()
        {
            ConfigTools.InitConf("Config.xml");
            try
            {
                ConfigTools.SetLanguage("coucou");
            }
            catch (LanguageException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(LanguageException))]
        public void TestNullCultureInfo()
        {
            ConfigTools.InitConf("Config.xml");
            try
            {
                ConfigTools.SetLanguage((CultureInfo)null);
            }
            catch (LanguageException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        [TestMethod]
        public void TestAvailableLanguages()
        {
            ConfigTools.InitConf("Config.xml");
            ICollection<CultureInfo> languages = ConfigTools.GetAvailableLanguage();
            Assert.IsTrue(languages.Contains(CultureInfo.GetCultureInfo("en")));
            Assert.IsTrue(languages.Contains(CultureInfo.GetCultureInfo("fr")));
        }
    }
}
