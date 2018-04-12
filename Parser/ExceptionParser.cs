using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using LightNovelSniffer.Web;
using LightNovelSniffer.Web.Parser;

namespace LightNovelSniffer_Tests.Parser
{
    public class ExceptionParser : IParser
    {
        public LnChapter Parse(HtmlDocument doc)
        {
            throw new Exception();
        }

        public bool CanParse(string url)
        {
            return true;
        }
    }
}
