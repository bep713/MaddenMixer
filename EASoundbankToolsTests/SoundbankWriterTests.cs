using EASoundbankTools.Model;
using EASoundbankTools.Parser;
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
    public class SoundbankWriterTests
    {
        private string TestFilePathSbr_NewWave = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/sbr_nwr.sbr");
        private string TestFilePathSbs_NewWave = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/sbs_nwr.sbs");

        private string TestFilePathSbr_Harmony = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/sbr_harmony.sbr");
        private string TestFilePathSbs_Harmony = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/sbs_harmony.sbs");

        private string TestFilePathSbr_Standalone = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/sbr_standalone.sbr");

        private string TestFilePathSbrWrite_NewWave = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/write_test/sbr_nwr.sbr");
        private string TestFilePathSbsWrite_NewWave = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/write_test/sbs_nwr.sbs");

        private string TestFilePathSbrWrite_Harmony = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/write_test/sbr_harmony.sbr");
        private string TestFilePathSbsWrite_Harmony = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/write_test/sbs_harmony.sbs");

        private string TestFilePathSbrWrite_Standalone = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/write_test/sbr_standalone.sbr");

        private string SongToWrite = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/write_test/bruno.sbs");
        private string TestWritePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/SoundbankParser/write_test/write_test_output");

        private SoundbankParser Parser;
        private SoundbankWriter Writer;

        private ISoundbank Soundbank_NewWave;
        private ISoundbank Soundbank_Harmony;
        private ISoundbank Soundbank_Standalone;

        [SetUp]
        public void Setup()
        {
            Parser = new SoundbankParser();
            Writer = new SoundbankWriter();

            File.Copy(TestFilePathSbr_NewWave, TestFilePathSbrWrite_NewWave, true);
            File.Copy(TestFilePathSbs_NewWave, TestFilePathSbsWrite_NewWave, true);

            File.Copy(TestFilePathSbr_Harmony, TestFilePathSbrWrite_Harmony, true);
            File.Copy(TestFilePathSbs_Harmony, TestFilePathSbsWrite_Harmony, true);

            File.Copy(TestFilePathSbr_Standalone, TestFilePathSbrWrite_Standalone, true);

            Soundbank_NewWave = Parser.ParseSbrSbs(TestFilePathSbrWrite_NewWave, TestFilePathSbsWrite_NewWave);
            Soundbank_Harmony = Parser.ParseSbrSbs(TestFilePathSbrWrite_Harmony, TestFilePathSbsWrite_Harmony);
            Soundbank_Standalone = Parser.ParseSbrStandalone(TestFilePathSbrWrite_Standalone);
        }

        /* NEW WAVE RESOURCE ======================================================*/


        /* HARMONY ====================================================== */
        [Test]
        public void WriteSbrHarmony()
        {
            var entry = Soundbank_Harmony.Entries[0];
            ulong entryOffset = entry.Offset;
            ulong songToWriteLength = 0x23CE8C;

            Writer.WriteAndFixOffsets(Soundbank_Harmony, entry, SongToWrite, TestWritePath);

            string TestWritePathSbr = TestWritePath + ".sbr";
            string TestWritePathSbs = TestWritePath + ".sbs";
            Soundbank_Harmony = Parser.ParseSbrSbs(TestWritePathSbr, TestWritePathSbs);

            Assert.That(Soundbank_Harmony.Entries[0].Offset, Is.EqualTo(entryOffset));
            Assert.That(Soundbank_Harmony.Entries[2].Offset, Is.EqualTo(entryOffset + songToWriteLength));

            Assert.That(Soundbank_Harmony.Entries[14].SongOffset, Is.EqualTo(0));
            Assert.That(Soundbank_Harmony.Entries[15].SongOffset, Is.EqualTo(0));
            Assert.That(Soundbank_Harmony.Entries[39].SongOffset, Is.EqualTo(0));

            Assert.That(Soundbank_Harmony.Entries[16].SongOffset, Is.EqualTo(1));
            Assert.That(Soundbank_Harmony.Entries[17].SongOffset, Is.EqualTo(1));
            Assert.That(Soundbank_Harmony.Entries[48].SongOffset, Is.EqualTo(1));

            Assert.That(Soundbank_Harmony.Entries[12].SongOffset, Is.EqualTo(16));
            Assert.That(Soundbank_Harmony.Entries[13].SongOffset, Is.EqualTo(16));
            Assert.That(Soundbank_Harmony.Entries[38].SongOffset, Is.EqualTo(16));
            Assert.That(Soundbank_Harmony.Entries[51].SongOffset, Is.EqualTo(16));
        }

        /* STANDALONE ====================================================== */
        //[Test]
        //public void WriteSbrStandalone()
        //{
        //    int entryOffset = 0x5C;
        //    int songToWriteLength = 0x23CE8C;

        //    Writer.WriteAndFixOffsets(Soundbank_Standalone, Soundbank_Standalone.Entries[0], SongToWrite, TestWritePath);
        //    Soundbank_Standalone = Parser.ParseSbrStandalone(TestFilePathSbrWrite_Standalone);

        //    Assert.That(Soundbank_Standalone.Entries[0].Offset, Is.EqualTo(entryOffset));
        //    Assert.That(Soundbank_Standalone.Entries[3].Offset, Is.EqualTo(entryOffset + songToWriteLength));
        //}

        // PARSE SBR ======================================================


        // PARSE SBS =================================================

    }
}
