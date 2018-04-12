using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LightNovelSniffer.Config;
using LightNovelSniffer.Exception;
using LightNovelSniffer.Web;
using LightNovelSniffer.Web.Parser;
using LightNovelSniffer_Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightNovelSniffer_Tests.Parser
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void DefaultParser()
        {
            List<IParser> parsers = new ParserFactory().AvailableParsers.ToList();

            Assert.AreEqual(parsers.Count, 3);

            List<Type> parserTypes = parsers.Select(p => p.GetType()).ToList();

            Assert.IsTrue(parserTypes.Contains(typeof(GravityTaleParser)));
            Assert.IsTrue(parserTypes.Contains(typeof(WuxiaworldParser)));
            Assert.IsTrue(parserTypes.Contains(typeof(XiaowazParser)));
        }

        [TestMethod]
        public void RegisterExternalParserFromClass()
        {
            new ParserFactory().RegisterParser(new CustomParser());

            Assert.AreEqual(new ParserFactory().AvailableParsers.Count, 4);
            CheckParsers();
        }

        [TestMethod]
        public void RegisterExternalParserFromAssemblyAndClass()
        {
            new ParserFactory().RegisterParserFromAssemblyDllAndClass(GetType().Assembly.Location, typeof(CustomParser).FullName);

            Assert.AreEqual(new ParserFactory().AvailableParsers.Count, 4);
            CheckParsers();
        }

        [TestMethod]
        public void RegisterExternalParserFromAssembly()
        {
            new ParserFactory().RegisterAllParserFromAssembly(GetType().Assembly.Location);

            Assert.AreEqual(new ParserFactory().AvailableParsers.Count, 5);
            CheckParsers();
        }

        [TestMethod]
        public void RegisterExternalParserFromAssemblyFile()
        {
            new ParserFactory().RegisterAllParserFromAssembly(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase),
                    "LightNovelSniffer-Tests.dll").
                Substring(6)); // remove "file://" returned by Path.GetDirectoryName

            Assert.AreEqual(new ParserFactory().AvailableParsers.Count, 5);
            CheckParsers();
        }
        
        [TestMethod]
        [ExpectedException(typeof(DynamicParserException))]
        public void DynamicParserException()
        {
            new ParserFactory().RegisterParserFromAssemblyDllAndClass(GetType().Assembly.Location, typeof(ParserTests).FullName);
        }

        private void CheckParsers()
        {
            IParser xiaowazParser = new ParserFactory().GetParser("xiaowaz.fr");
            Assert.AreEqual(xiaowazParser.GetType(), typeof(XiaowazParser));

            IParser customParser = new ParserFactory().GetParser("LightNovelSnifferTests");
            Assert.AreEqual(customParser.GetType(), typeof(CustomParser));

            LnChapter chapter = customParser.Parse(null);
            ((CustomParser)customParser).test(chapter);
        }
    }
}