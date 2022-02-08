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
            Assert.That(SBRFile.Header.Endianness, Is.EqualTo(SBRFile.SBREndianness.Little));
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
            Assert.That(SBRFile.Header.SBRType, Is.EqualTo(SBRFile.SBRType.Harmony));
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

        [Test]
        public void ParseDSetRecords()
        {
            Assert.That(SBRFile.DSets[0].Records.Count, Is.EqualTo(25));
        }

        [Test]
        public void ParseDSetFields()
        {
            Assert.That(SBRFile.DSets[0].Records[0].Fields.Count, Is.EqualTo(3));
        }

        [Test]
        public void ParseDSetFieldDefinitionId()
        {
            Assert.That(SBRFile.DSets[0].Definitions[0].Id, Is.EqualTo(776947270));
        }

        [Test]
        public void ParseDSetFieldDefinitionName()
        {
            Assert.That(SBRFile.DSets[0].Definitions[0].Name, Is.EqualTo("OFF"));
        }

        [Test]
        public void ParseDSetFieldDefinitionDataTypeRaw()
        {
            Assert.That(SBRFile.DSets[0].Definitions[0].DataTypeRaw, Is.EqualTo(3));
        }

        [Test]
        public void ParseDSetFieldDefinitionDataType()
        {
            Assert.That(SBRFile.DSets[0].Definitions[0].DataType, Is.EqualTo(DSetFieldDefinition.FieldType.Int64));
        }

        [Test]
        public void ParseDSetFieldDefinitionStoreType()
        {
            Assert.That(SBRFile.DSets[0].Definitions[0].StoreType, Is.EqualTo(2));
        }

        [Test]
        public void ParseDSetFieldDefinitionStoreParam1()
        {
            Assert.That(SBRFile.DSets[0].Definitions[0].StoreParam1, Is.EqualTo(1024));
        }

        [Test]
        public void ParseDSetFieldDefinitionStoreParam2()
        {
            Assert.That(SBRFile.DSets[0].Definitions[0].StoreParam2, Is.EqualTo(32));
        }

        [Test]
        public void ParseDSetFieldDefinitionTableOffset()
        {
            Assert.That(SBRFile.DSets[0].Definitions[0].TableOffset, Is.EqualTo(552));
        }

        [Test]
        public void ParseDSetFieldDefinitionUnknown()
        {
            Assert.That(SBRFile.DSets[0].Definitions[0].Unknown, Is.EqualTo(488));
        }

        [Test]
        public void ParseDSetFieldValues_Offset()
        {
            Assert.That(SBRFile.DSets[0].Records[0].Fields[0].GetValue<int>, Is.EqualTo(46257760));
            Assert.That(SBRFile.DSets[0].Records[1].Fields[0].GetValue<int>, Is.EqualTo(51296768));
            Assert.That(SBRFile.DSets[0].Records[2].Fields[0].GetValue<int>, Is.EqualTo(54968256));
            Assert.That(SBRFile.DSets[0].Records[SBRFile.DSets[0].Records.Count - 1].Fields[0].GetValue<int>, Is.EqualTo(36713568));
        }

        [Test]
        public void ParseDSetFieldValues_Offset_ULongValue()
        {
            Assert.That(SBRFile.DSets[0].Records[0].Fields[0].Value, Is.EqualTo(46257760));
        }

        [Test]
        public void ParseDSetFieldValues_Sbs()
        {
            Assert.That(SBRFile.DSets[0].Records[0].Fields[1].Value, Is.EqualTo(6));
            Assert.That(SBRFile.DSets[0].Records[1].Fields[1].Value, Is.EqualTo(6));
            Assert.That(SBRFile.DSets[0].Records[2].Fields[1].Value, Is.EqualTo(6));
            Assert.That(SBRFile.DSets[0].Records[SBRFile.DSets[0].Records.Count - 1].Fields[1].Value, Is.EqualTo(6));
        }

        [Test]
        public void ParseDSetFieldValues_Sid()
        {
            Assert.That(SBRFile.DSets[0].Records[0].Fields[2].Value, Is.EqualTo(310405));
            Assert.That(SBRFile.DSets[0].Records[1].Fields[2].Value, Is.EqualTo(310406));
            Assert.That(SBRFile.DSets[0].Records[2].Fields[2].Value, Is.EqualTo(310407));
            Assert.That(SBRFile.DSets[0].Records[SBRFile.DSets[0].Records.Count - 1].Fields[2].Value, Is.EqualTo(310380));
        }

        [Test]
        public void GetFieldByName()
        {
            var offset1 = SBRFile.DSets[0].Records[0].GetFieldByName("OFF");
            Assert.That(offset1.Value, Is.EqualTo(46257760));
        }

        [Test]
        public void GetFieldValueByName()
        {
            Assert.That(SBRFile.DSets[0].Records[0].GetFieldValueByName("OFF"), Is.EqualTo(46257760));
        }
    }
}