using EASoundbankTools.Model;
using EASoundbankTools.Parser;
using EASoundbankTools.Parser.SBS;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EASoundbankTools.Model.Enums;

namespace EASoundbankToolsTests
{
    [TestFixture]
    public class SBSParserTests
    {
        private string SbrFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/sbr_harmony.sbr");
        private string TestFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/sbs_harmony.sbs");

        private SoundbankParser SoundbankParser = new SoundbankParser();
        private SBSParser Parser = new SBSParser();
        private SBSFile SBSFile;

        [SetUp]
        public void Setup()
        {
            ISoundbank soundbank = SoundbankParser.ParseSbr(SbrFilePath);
            SBSFile = Parser.ParseFromEntries(TestFilePath, soundbank.Entries);
        }

        [Test]
        public void ParseNumberOfEntries()
        {
            Assert.That(SBSFile.Blocks.Count, Is.EqualTo(56));
        }

        [Test]
        public void ParseHeaderVersion()
        {
            Assert.That(SBSFile.Blocks[0].Header.Version, Is.EqualTo(EAAudioVersion.Version1));
        }

        [Test]
        public void ParseHeaderCodec()
        {
            Assert.That(SBSFile.Blocks[0].Header.Codec, Is.EqualTo(EAAudioCodec.EALayer3_V2_PCM));
        }

        [Test]
        public void ParseHeaderType()
        {
            Assert.That(SBSFile.Blocks[0].Header.Type, Is.EqualTo(EAAudioType.Stream));
        }

        [Test]
        public void ParseHeaderSampleRate()
        {
            Assert.That(SBSFile.Blocks[0].Header.SampleRate, Is.EqualTo(44100));
        }

        [Test]
        public void ParseHeaderChannels()
        {
            Assert.That(SBSFile.Blocks[0].Header.Channels, Is.EqualTo(2));
        }

        [Test]
        public void ParseHeaderLoopFlag()
        {
            Assert.That(SBSFile.Blocks[0].Header.LoopFlag, Is.EqualTo(0));
        }

        [Test]
        public void ParseHeaderNumberOfSamples()
        {
            Assert.That(SBSFile.Blocks[0].Header.NumberOfSamples, Is.EqualTo(464810));
        }
    }
}
