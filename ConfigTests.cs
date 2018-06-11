using System;
using System.Linq;
using LightNovelSniffer.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightNovelSniffer_Tests
{
    [TestClass]
    public class ConfigTests
    {
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            ConfigTools.InitConf("Config.xml");
            ConfigTools.InitLightNovels("LightNovels.xml");
        }

        [TestMethod]
        public void VerifGlobals()
        {
            Assert.AreEqual(Globale.OUTPUT_FOLDER, "outputFolder");
            Assert.IsTrue(Globale.INTERACTIVE_MODE);
            Assert.AreEqual(Globale.PUBLISHER, "publisher");
            Assert.AreEqual(Globale.DEFAULT_CHAPTER_TITLE, "defaultChapterTitle");
            Assert.AreEqual(Globale.MAX_CHAPTER_ON_ERROR_COUNT_BEFORE_STOP, 10);
        }

        [TestMethod]
        public void VerifOverride()
        {
            ConfigTools.InitConf("Config_override.xml");
            Assert.AreEqual(Globale.OUTPUT_FOLDER, "outputFolder_override");
            Assert.IsFalse(Globale.INTERACTIVE_MODE);
            Assert.AreEqual(Globale.PUBLISHER, "publisher_override");
            Assert.AreEqual(Globale.DEFAULT_CHAPTER_TITLE, "defaultChapterTitle_override");
            Assert.AreEqual(Globale.MAX_CHAPTER_ON_ERROR_COUNT_BEFORE_STOP, 20);
        }

        [TestMethod]
        public void VerifLn()
        {
            Assert.IsNotNull(Globale.LN_TO_RETRIEVE);
            Assert.AreEqual(Globale.LN_TO_RETRIEVE.Count, 1);

            LnParameters ln = Globale.LN_TO_RETRIEVE.First();

            Assert.AreEqual(ln.name, "lnName");
            
            Assert.IsNotNull(ln.authors);
            Assert.AreEqual(ln.authors.Count, 2);
            Assert.AreEqual(ln.authors[0], "author 1");
            Assert.AreEqual(ln.authors[1], "author 2");

            Assert.AreEqual(ln.urlCover, "http://google.com/image.jpg");

            Assert.IsNotNull(ln.urlParameters);
            Assert.AreEqual(ln.urlParameters.Count, 1);
            Assert.AreEqual(ln.urlParameters[0].url, "http://google.com/Novel/chapter-{0}");
            Assert.AreEqual(ln.urlParameters[0].language, "EN");
            Assert.AreEqual(ln.urlParameters[0].firstChapterNumber, 1);
            Assert.AreEqual(ln.urlParameters[0].lastChapterNumber, 10);
        }

        [TestMethod]
        public void VerifLnAddAndReset()
        {
            ConfigTools.InitLightNovels("LightNovels.xml");
            Assert.AreEqual(Globale.LN_TO_RETRIEVE.Count, 2);
            ConfigTools.InitLightNovels("LightNovels.xml", true);
            Assert.AreEqual(Globale.LN_TO_RETRIEVE.Count, 1);
        }
    }
}
