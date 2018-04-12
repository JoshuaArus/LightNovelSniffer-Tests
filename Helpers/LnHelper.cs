using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LightNovelSniffer.Config;

namespace LightNovelSniffer_Tests.Helpers
{
    internal class LnHelper
    {
        public static LnParameters GetTestLnParametersWithCover()
        {
            return new LnParameters
            {
                name = "name",
                urlCover = "http://google.fr/image.png",
                authors = new List<string> {"author 1", "author 2"},

                urlParameters = new List<UrlParameter>
                {
                    new UrlParameter()
                    {
                        firstChapterNumber = 1,
                        lastChapterNumber = 10,
                        language = "EN",
                        url = "http://google.com"
                    },
                    new UrlParameter()
                    {
                        firstChapterNumber = 11,
                        lastChapterNumber = 20,
                        language = "FR",
                        url = "http://google.com"
                    }
                }
            };
        }

        public static LnParameters GetTestLnParameters()
        {
            return new LnParameters
            {
                name = "name",
                authors = new List<string> { "author 1", "author 2" },

                urlParameters = new List<UrlParameter>
                {
                    new UrlParameter()
                    {
                        firstChapterNumber = 1,
                        lastChapterNumber = 10,
                        language = "EN",
                        url = "http://google.com"
                    },
                    new UrlParameter()
                    {
                        firstChapterNumber = 11,
                        lastChapterNumber = 20,
                        language = "FR",
                        url = "http://google.com"
                    }
                }
            };
        }
    }
}
