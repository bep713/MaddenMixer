using EASoundbankTools.Model.SBR;
using EASoundbankTools.Parser.SBR;
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
    public class StoreType2StrategyTests
    {
        private StoreType2Strategy strategy;

        [SetUp]
        public void Setup()
        {
            strategy = new StoreType2Strategy();
        }

        [Test]
        public void Parse()
        {
            byte[] contents = { 0x00, 0x00, 0x88, 0x54, 0x97, 0x67, 0xF9, 0x12, 0x00, 0x00,
                0xF9, 0x12, 0x83, 0x44, 0x81, 0x2B, 0x83, 0x44, 0x81, 0x2B, 0x81, 0x2B, 0x83, 0x44 };

            using var stream = new MemoryStream(contents);
            using var reader = new BinaryReader(stream);

            DSetFieldDefinition definition = new DSetFieldDefinition();
            definition.StoreParam1 = 0x202;
            definition.StoreParam2 = 0x4;
            definition.TableOffset = 0;

            var field1 = strategy.ParseValue(reader, definition, 0);
            Assert.That(field1.Value, Is.EqualTo(4));

            var field2 = strategy.ParseValue(reader, definition, 1);
            Assert.That(field2.Value, Is.EqualTo(86564));

            var field3 = strategy.ParseValue(reader, definition, 2);
            Assert.That(field3.Value, Is.EqualTo(106080));
        }

        [Test]
        public void Write()
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            DSetFieldDefinition definition = new DSetFieldDefinition();
            definition.DataTypeRaw = (byte)DSetFieldDefinition.FieldType.UInt32;
            definition.StoreParam1 = 0x202;
            definition.StoreParam2 = 0x4;
            definition.TableOffset = 0;

            strategy.WriteValue(writer, definition, 4);
            strategy.WriteValue(writer, definition, 86564);
            strategy.WriteValue(writer, definition, 106080);

            using var reader = new BinaryReader(stream);
            reader.BaseStream.Position = 0;

            Assert.That(reader.ReadUInt16(), Is.EqualTo(0));   // Base offset = 4, so subtract that.            
            Assert.That(reader.ReadUInt16(), Is.EqualTo(0x5488));
            Assert.That(reader.ReadUInt16(), Is.EqualTo(0x6797));
        }
    }
}
