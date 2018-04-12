using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using LightNovelSniffer.Web;
using LightNovelSniffer.Web.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightNovelSniffer_Tests.Parser
{
    public class CustomParser : IParser
    {
        public LnChapter Parse(HtmlDocument doc)
        {
            List<HtmlNode> paragraphs = new List<HtmlNode>();
            HtmlNode p = HtmlTextNode.CreateNode("innerText");
            paragraphs.Add(p);
            LnChapter c = new LnChapter("title", paragraphs);
            c.chapNumber = 12;
            return c;
        }

        public bool CanParse(string url)
        {
            return url.Contains("customParser");
        }

        public void test(LnChapter chapter)
        {
            Assert.AreEqual(chapter.title, "title");
            Assert.AreEqual(chapter.paragraphs.Count, 1);
            Assert.AreEqual(chapter.paragraphs.First().InnerText, "innerText");
            Assert.AreEqual(chapter.chapNumber, 12);
        }
    }
}
