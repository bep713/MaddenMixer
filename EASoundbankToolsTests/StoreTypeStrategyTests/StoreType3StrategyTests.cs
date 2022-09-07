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
    public class StoreType3StrategyTests
    {
        private StoreType3Strategy strategy;

        [SetUp]
        public void Setup()
        {
            strategy = new StoreType3Strategy();
        }

        [Test]
        public void Parse()
        {
            byte[] contents = { 0x20, 0x00, 0x00, 0x00, 0xA0, 0x9C, 0x0D, 0x00 };

            using var stream = new MemoryStream(contents);
            using var reader = new BinaryReader(stream);

            DSetFieldDefinition definition = new DSetFieldDefinition();
            definition.StoreParam1 = 0x401;
            definition.StoreParam2 = 0x2;
            definition.TableOffset = 0;

            var field1 = strategy.ParseValue(reader, definition, 0);
            Assert.That(field1.Value, Is.EqualTo(32));

            var field2 = strategy.ParseValue(reader, definition, 1);
            Assert.That(field2.Value, Is.EqualTo(892064));

            var field3 = strategy.ParseValue(reader, definition, 2);
            Assert.That(field3, Is.Null);
        }

        [Test]
        public void Write()
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            DSetFieldDefinition definition = new DSetFieldDefinition();
            definition.DataTypeRaw = (byte)DSetFieldDefinition.FieldType.Int64;
            definition.StoreParam1 = 0x401;
            definition.StoreParam2 = 0x2;
            definition.TableOffset = 0;

            strategy.WriteValue(writer, definition, 32);
            strategy.WriteValue(writer, definition, 892064);

            using var reader = new BinaryReader(stream);
            reader.BaseStream.Position = 0;

            Assert.That(reader.ReadUInt32(), Is.EqualTo(32));   // Base offset = 4, so subtract that.            
            Assert.That(reader.ReadUInt32(), Is.EqualTo(892064));
        }
    }
}
