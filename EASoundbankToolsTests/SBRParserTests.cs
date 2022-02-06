using EASoundbankTools.Model.SBR;
using EASoundbankTools.Parser.SBR;
using NUnit.Framework;
using System.IO;

namespace EASoundbankToolsTests
{
    [TestFixture]
    public class SBRParserTests
    {
        private string TestFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/SBRParser/sbr_test.sbr");
        private SBRParser Parser;
        private SBRFile SBRFile;

        [SetUp]
        public void Setup()
        {
            Parser = new SBRParser();
            SBRFile = Parser.Parse(TestFilePath);
        }

        [Test]
        public void ParseHeaderMagic()
        {
            Assert.That(SBRFile.Header.Magic, Is.EqualTo(1396862053));
        }

        [Test]
        public void ParseHeaderEndianness()
        {
            Assert.That(SBRFile.Header.Endianness, Is.EqualTo(SBREndianness.Little));
        }
        
        [Test]
        public void ParseNumDSets()
        {
            Assert.That(SBRFile.Header.NumberOfDSets, Is.EqualTo(1));
        }    
        
        [Test]
        public void ParseSBRTypeRaw()
        {
            Assert.That(SBRFile.Header.SBRTypeRaw, Is.EqualTo(1212240461));
        }     
        
        [Test]
        public void ParseSBRType()
        {
            Assert.That(SBRFile.Header.SBRType, Is.EqualTo(SBRType.Harmony));
        }     
        
        [Test]
        public void ParseTableOffset()
        {
            Assert.That(SBRFile.Header.TableOffset, Is.EqualTo(80));
        }     
        
        [Test]
        public void ParseDataOffset()
        {
            Assert.That(SBRFile.Header.DataOffset, Is.EqualTo(88));
        }    
        
        [Test]
        public void ParseDSet()
        {
            Assert.That(SBRFile.DSets[0], Is.Not.Null);
        }  
        
        [Test]
        public void ParseDSetMagic()
        {
            Assert.That(SBRFile.DSets[0].Magic, Is.EqualTo(0x44534554));
        }
        
        [Test]
        public void ParseDSetId()
        {
            Assert.That(SBRFile.DSets[0].Id, Is.EqualTo(0xF9060550));
        }
        
        [Test]
        public void ParseDSetDataOffset()
        {
            Assert.That(SBRFile.DSets[0].DataOffset, Is.EqualTo(0x58));
        }

        [Test]
        public void ParseDSetNumberOfElements()
        {
            Assert.That(SBRFile.DSets[0].NumberOfElements, Is.EqualTo(0x19));
        }

        [Test]
        public void ParseDSetNumberOfFields()
        {
            Assert.That(SBRFile.DSets[0].NumberOfFields, Is.EqualTo(0x3));
        }
    }
}