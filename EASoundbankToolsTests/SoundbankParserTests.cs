using EASoundbankTools.Model;
using EASoundbankTools.Parser;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASoundbankToolsTests
{
    [TestFixture]
    public class SoundbankParserTests
    {
        private string TestFilePathSbr_NewWave = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/sbr_nwr.sbr");
        private string TestFilePathSbs_NewWave = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/sbs_nwr.sbs");
        
        private string TestFilePathSbr_Harmony = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/sbr_harmony.sbr");
        private string TestFilePathSbs_Harmony = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/sbs_harmony.sbs");

        private string TestFilePathSbr_Standalone = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/sbr_standalone.sbr");

        private SoundbankParser Parser;
        private ISoundbank Soundbank_NewWave;
        private ISoundbank Soundbank_Harmony;
        private ISoundbank Soundbank_Standalone;

        private ISoundbank Soundbank_Parse_Standalone;
        private ISoundbank Soundbank_Parse_NonStandalone_Harmony;
        private ISoundbank Soundbank_Parse_NonStandalone_NewWave;

        [SetUp]
        public void Setup()
        {
            Parser = new SoundbankParser();
            Soundbank_NewWave = Parser.ParseSbrSbs(TestFilePathSbr_NewWave, TestFilePathSbs_NewWave);
            Soundbank_Harmony = Parser.ParseSbrSbs(TestFilePathSbr_Harmony, TestFilePathSbs_Harmony);
            Soundbank_Standalone = Parser.ParseSbrStandalone(TestFilePathSbr_Standalone);

            Soundbank_Parse_Standalone = Parser.ParseSbr(TestFilePathSbr_Standalone);
            Soundbank_Parse_NonStandalone_Harmony = Parser.ParseSbr(TestFilePathSbr_Harmony);
            Soundbank_Parse_NonStandalone_NewWave = Parser.ParseSbr(TestFilePathSbr_NewWave);
        }

        /* NEW WAVE RESOURCE ======================================================*/
        [Test]
        public void ParseSoundbankSbrSbs_NumberOfEntries_NewWave()
        {
            Assert.That(Soundbank_NewWave.Entries.Count, Is.EqualTo(7));
        }

        [Test]
        public void ParseSoundbankSbrSbs_EntryOffsets_NewWave()
        {
            Assert.That(Soundbank_NewWave.Entries[0].Offset, Is.EqualTo(0));
            Assert.That(Soundbank_NewWave.Entries[1].Offset, Is.EqualTo(47420));
            Assert.That(Soundbank_NewWave.Entries[2].Offset, Is.EqualTo(77880));
            Assert.That(Soundbank_NewWave.Entries[6].Offset, Is.EqualTo(157544));
        }

        [Test]
        public void ParseSoundbankSbrSbs_SbrPath_NewWave()
        {
            Assert.That(Soundbank_NewWave.SbrPath, Is.EqualTo(TestFilePathSbr_NewWave));
        }

        [Test]
        public void ParseSoundbankSbrSbs_SbsPath_NewWave()
        {
            Assert.That(Soundbank_NewWave, Is.TypeOf(typeof(Soundbank_SbrSbs)));
            Assert.That(((Soundbank_SbrSbs)Soundbank_NewWave).SbsPath, Is.EqualTo(TestFilePathSbs_NewWave));
        }

        [Test]
        public void ParseSoundbankSbrSbs_RawOffsetInSbr_NewWave()
        {
            Assert.That(Soundbank_NewWave.Entries[0].RawOffsetInSbr, Is.EqualTo(3));
            Assert.That(Soundbank_NewWave.Entries[1].RawOffsetInSbr, Is.EqualTo(47423));
            Assert.That(Soundbank_NewWave.Entries[2].RawOffsetInSbr, Is.EqualTo(77883));
            Assert.That(Soundbank_NewWave.Entries[6].RawOffsetInSbr, Is.EqualTo(157547));
        }

        [Test]
        public void ParseSoundbankSbrSbs_Name_NewWave()
        {
            Assert.That(Soundbank_NewWave.Entries[0].Name, Is.EqualTo("0"));
            Assert.That(Soundbank_NewWave.Entries[1].Name, Is.EqualTo("1"));
            Assert.That(Soundbank_NewWave.Entries[2].Name, Is.EqualTo("2"));
            Assert.That(Soundbank_NewWave.Entries[6].Name, Is.EqualTo("6"));
        }
        
        /* HARMONY ====================================================== */
        [Test]
        public void ParseSoundbankSbrSbs_NumberOfEntries_Harmony()
        {
            Assert.That(Soundbank_Harmony.Entries.Count, Is.EqualTo(56));
        }

        [Test]
        public void ParseSoundbankSbrSbs_EntryOffsets_Harmony()
        {
            Assert.That(Soundbank_Harmony.Entries[0].Offset, Is.EqualTo(878368));
            Assert.That(Soundbank_Harmony.Entries[2].Offset, Is.EqualTo(989888));
            Assert.That(Soundbank_Harmony.Entries[4].Offset, Is.EqualTo(1123872));
            Assert.That(Soundbank_Harmony.Entries[55].Offset, Is.EqualTo(1787904));
        }

        [Test]
        public void ParseSoundbankSbrSbs_SbrPath_Harmony()
        {
            Assert.That(Soundbank_Harmony.SbrPath, Is.EqualTo(TestFilePathSbr_Harmony));
        }

        [Test]
        public void ParseSoundbankSbrSbs_SbsPath_Harmony()
        {
            Assert.That(Soundbank_Harmony, Is.TypeOf(typeof(Soundbank_SbrSbs)));
            Assert.That(((Soundbank_SbrSbs)Soundbank_Harmony).SbsPath, Is.EqualTo(TestFilePathSbs_Harmony));
        }

        [Test]
        public void ParseSoundbankSbrSbs_RawOffsetInSbr_Harmony()
        {
            Assert.That(Soundbank_Harmony.Entries[0].RawOffsetInSbr, Is.EqualTo(878368));
            Assert.That(Soundbank_Harmony.Entries[2].RawOffsetInSbr, Is.EqualTo(989888));
            Assert.That(Soundbank_Harmony.Entries[4].RawOffsetInSbr, Is.EqualTo(1123872));
            Assert.That(Soundbank_Harmony.Entries[55].RawOffsetInSbr, Is.EqualTo(1787904));
        }

        [Test]
        public void ParseSoundbankSbrSbs_Name_Harmony()
        {
            Assert.That(Soundbank_Harmony.Entries[0].Name, Is.EqualTo("0"));
            Assert.That(Soundbank_Harmony.Entries[2].Name, Is.EqualTo("2"));
            Assert.That(Soundbank_Harmony.Entries[4].Name, Is.EqualTo("4"));
            Assert.That(Soundbank_Harmony.Entries[55].Name, Is.EqualTo("55"));
        }

        /* STANDALONE ====================================================== */
        [Test]
        public void ParseSoundbankSbrSbs_NumberOfEntries_Standalone()
        {
            Assert.That(Soundbank_Standalone.Entries.Count, Is.EqualTo(12));
        }

        [Test]
        public void ParseSoundbankSbrSbs_EntryOffsets_Standalone()
        {
            Assert.That(Soundbank_Standalone.Entries[0].Offset, Is.EqualTo(92));
            Assert.That(Soundbank_Standalone.Entries[1].Offset, Is.EqualTo(86652));
            Assert.That(Soundbank_Standalone.Entries[2].Offset, Is.EqualTo(106168));
            Assert.That(Soundbank_Standalone.Entries[11].Offset, Is.EqualTo(70248));
        }

        [Test]
        public void ParseSoundbankSbrSbs_SbrPath_Standalone()
        {
            Assert.That(Soundbank_Standalone.SbrPath, Is.EqualTo(TestFilePathSbr_Standalone));
        }

        [Test]
        public void ParseSoundbankSbrSbs_RawOffsetInSbr_Standalone()
        {
            Assert.That(Soundbank_Standalone.Entries[0].RawOffsetInSbr, Is.EqualTo(4));
            Assert.That(Soundbank_Standalone.Entries[1].RawOffsetInSbr, Is.EqualTo(86564));
            Assert.That(Soundbank_Standalone.Entries[2].RawOffsetInSbr, Is.EqualTo(106080));
            Assert.That(Soundbank_Standalone.Entries[11].RawOffsetInSbr, Is.EqualTo(70160));
        }

        [Test]
        public void ParseSoundbankSbrSbs_Name_Standalone()
        {
            Assert.That(Soundbank_Standalone.Entries[0].Name, Is.EqualTo("0"));
            Assert.That(Soundbank_Standalone.Entries[1].Name, Is.EqualTo("1"));
            Assert.That(Soundbank_Standalone.Entries[2].Name, Is.EqualTo("2"));
            Assert.That(Soundbank_Standalone.Entries[11].Name, Is.EqualTo("11"));
        }

        // PARSE SBR ======================================================
        [Test]
        public void ParseSbr_Standalone()
        {
            Assert.That(Soundbank_Parse_Standalone, Is.TypeOf(typeof(Soundbank_SbrStandalone)));

            Assert.That(Soundbank_Parse_Standalone.Entries[0].RawOffsetInSbr, Is.EqualTo(4));
            Assert.That(Soundbank_Parse_Standalone.Entries[1].RawOffsetInSbr, Is.EqualTo(86564));
            Assert.That(Soundbank_Parse_Standalone.Entries[2].RawOffsetInSbr, Is.EqualTo(106080));
            Assert.That(Soundbank_Parse_Standalone.Entries[11].RawOffsetInSbr, Is.EqualTo(70160));
        }

        [Test]
        public void ParseSbr_SbrSbs_Harmony()
        {
            Assert.That(Soundbank_Parse_NonStandalone_Harmony, Is.TypeOf(typeof(Soundbank_SbrSbs)));

            Assert.That(Soundbank_Parse_NonStandalone_Harmony.Entries[0].Offset, Is.EqualTo(878368));
            Assert.That(Soundbank_Parse_NonStandalone_Harmony.Entries[2].Offset, Is.EqualTo(989888));
            Assert.That(Soundbank_Parse_NonStandalone_Harmony.Entries[4].Offset, Is.EqualTo(1123872));
            Assert.That(Soundbank_Parse_NonStandalone_Harmony.Entries[55].Offset, Is.EqualTo(1787904));
        }

        [Test]
        public void ParseSbr_SbrSbs_NewWave()
        {
            Assert.That(Soundbank_Parse_NonStandalone_NewWave, Is.TypeOf(typeof(Soundbank_SbrSbs)));

            Assert.That(Soundbank_Parse_NonStandalone_NewWave.Entries[0].Offset, Is.EqualTo(0));
            Assert.That(Soundbank_Parse_NonStandalone_NewWave.Entries[1].Offset, Is.EqualTo(47420));
            Assert.That(Soundbank_Parse_NonStandalone_NewWave.Entries[2].Offset, Is.EqualTo(77880));
            Assert.That(Soundbank_Parse_NonStandalone_NewWave.Entries[6].Offset, Is.EqualTo(157544));
        }
    }
}
