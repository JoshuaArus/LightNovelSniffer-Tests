using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LightNovelSniffer.Config;
using LightNovelSniffer.Web;
using LightNovelSniffer.Web.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightNovelSniffer_Tests.Parser
{
    [TestClass]
    public class ParserTests
    {
        public static ParserFactory factory;

        [TestInitialize()]
        public void Initialize()
        {
            ConfigTools.InitConf();
            factory = new ParserFactory();
        }

        [TestMethod]
        public void DefaultParser()
        {
            List<IParser> parsers = factory.AvailableParsers.ToList();

            Assert.AreEqual(parsers.Count, 3);

            List<Type> parserTypes = parsers.Select(p => p.GetType()).ToList();

            Assert.IsTrue(parserTypes.Contains(typeof(GravityTaleParser)));
            Assert.IsTrue(parserTypes.Contains(typeof(WuxiaworldParser)));
            Assert.IsTrue(parserTypes.Contains(typeof(XiaowazParser)));
        }

        [TestMethod]
        public void RegisterExternalParserFromClass()
        {
            factory.RegisterParser(new CustomParser());

            Assert.AreEqual(factory.AvailableParsers.Count, 4);
            CheckParsers();
        }

        [TestMethod]
        public void RegisterExternalParserFromAssemblyAndClass()
        {
            factory.RegisterParserFromAssemblyDllAndClass(GetType().Assembly.Location, typeof(CustomParser).FullName);

            Assert.AreEqual(factory.AvailableParsers.Count, 4);
            CheckParsers();
        }

        [TestMethod]
        public void RegisterExternalParserFromAssembly()
        {
            factory.RegisterAllParserFromAssembly(GetType().Assembly.Location);

            Assert.AreEqual(factory.AvailableParsers.Count, 4);
            CheckParsers();
        }

        [TestMethod]
        public void RegisterExternalParserFromAssemblyFile()
        {
            factory.RegisterAllParserFromAssembly(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase),
                    "LightNovelSniffer-Tests.dll").
                Substring(6)); // remove "file://" returned by Path.GetDirectoryName

            Assert.AreEqual(factory.AvailableParsers.Count, 4);
            CheckParsers();
        }

        private void CheckParsers()
        {
            IParser xiaowazParser = factory.GetParser("xiaowaz.fr");
            Assert.AreEqual(xiaowazParser.GetType(), typeof(XiaowazParser));

            IParser customParser = factory.GetParser("LightNovelSnifferTests");
            Assert.AreEqual(customParser.GetType(), typeof(CustomParser));

            LnChapter chapter = customParser.Parse(null);
            ((CustomParser)customParser).test(chapter);
        }
    }
}