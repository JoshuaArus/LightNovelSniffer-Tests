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
            ConfigTools.InitConf();
            ConfigTools.InitLightNovels();
        }

        [TestMethod]
        public void VerifGlobals()
        {
            Assert.AreEqual(Globale.OUTPUT_FOLDER, "outputFolder");
            Assert.IsTrue(Globale.INTERACTIVE_MODE);
            Assert.AreEqual(Globale.PUBLISHER, "publisher");
            Assert.AreEqual(Globale.DEFAULT_CHAPTER_TITLE, "defaultChapterTitle");
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
    }
}
