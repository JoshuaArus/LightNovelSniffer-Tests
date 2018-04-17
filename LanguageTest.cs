using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using LightNovelSniffer.Config;
using LightNovelSniffer.Exception;
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
            ConfigTools.InitConf();
            CultureInfo ci = ConfigTools.GetCurrentLanguage();
            Assert.AreEqual(ci, CultureInfo.CurrentCulture);
        }

        [TestMethod]
        public void TestDefaultLanguageChange()
        {
            CultureInfo ci = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpper() == "FR" ? CultureInfo.GetCultureInfo("en-US") : CultureInfo.GetCultureInfo("de-DE");

            ConfigTools.InitConf(ci);
            Assert.AreEqual(ci, ConfigTools.GetCurrentLanguage());
        }

        [TestMethod]
        [ExpectedException(typeof(CoverException))]
        public void TestDefaultLanguageMessages()
        {
            string url = "coucou";
            ConfigTools.InitConf();
            try
            {
                WebCrawler.DownloadCover(url);
            }
            catch (CoverException e)
            {
                Console.WriteLine(e.Message.Equals(string.Format("Impossible de télécharger la cover {0}", url)));
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CoverException))]
        public void TestSpecifiedLanguageMessages()
        {
            string url = "coucou";
            ConfigTools.InitConf(CultureInfo.GetCultureInfo("en-US"));
            try
            {
                WebCrawler.DownloadCover(url);
            }
            catch (CoverException e)
            {
                Console.WriteLine(e.Message.Equals(string.Format("Unable to download cover at {0}", url)));
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(LanguageException))]
        public void TestEmptyLanguageString()
        {
            ConfigTools.InitConf();
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
            ConfigTools.InitConf();            
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
            ConfigTools.InitConf();
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
            ConfigTools.InitConf();
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
    }
}
