using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LightNovelSniffer.Config;
using LightNovelSniffer.Exception;
using LightNovelSniffer.Web;
using LightNovelSniffer.Web.Parser;
using LightNovelSniffer_Tests.Helpers;
using LightNovelSniffer_Tests.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightNovelSniffer_Tests
{
    [TestClass]
    public class WebCrawlerTester
    {
        [TestInitialize()]
        public void Initialize()
        {
            ConfigTools.InitConf("Config.xml");
            Globale.INTERACTIVE_MODE = false;
        }

        [TestMethod]
        [ExpectedException(typeof(CoverException))]
        public void CoverExceptionWrongUri()
        {
            WebCrawler.DownloadCover("test");
        }

        [TestMethod]
        [ExpectedException(typeof(CoverException))]
        public void CoverException()
        {
            WebCrawler.DownloadCover("http://test");
        }
    }
}
