using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LightNovelSniffer.Libs;

namespace LightNovelSniffer_Tests.Helpers
{
    internal class WebCrawlerInterface : IOutput, IInput
    {
        public void Log(string text)
        {
            
        }

        public void Progress(string text)
        {
            
        }

        public bool Ask(string question)
        {
            return true;
        }

        public bool AskNegative(string question)
        {
            return true;
        }

        public string AskInformation(string question)
        {
            return "";
        }

        public string AskUrl(string question)
        {
            return "http://google.fr";
        }
    }

    internal class WebCrawlerHelper
    {
        private static WebCrawlerInterface _wci;

        internal static WebCrawlerInterface GetWebCrawlerInterface()
        {
            return _wci ?? (_wci = new WebCrawlerInterface());
        }
    }
}
