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
    public class StoreType1StrategyTests
    {
        private StoreType1Strategy strategy;

        [SetUp]
        public void Setup()
        {
            strategy = new StoreType1Strategy();
        }

        [Test]
        public void Parse()
        {
            using var stream = new MemoryStream();
            using var reader = new BinaryReader(stream);

            DSetFieldDefinition definition = new DSetFieldDefinition();
            definition.StoreParam1 = 1;
            definition.StoreParam2 = 20;
            definition.TableOffset = 0;

            var field1 = strategy.ParseValue(reader, definition, 0);
            Assert.That(field1.Value, Is.EqualTo(20));

            var field2 = strategy.ParseValue(reader, definition, 1);
            Assert.That(field2.Value, Is.EqualTo(21));

            var field20 = strategy.ParseValue(reader, definition, 19);
            Assert.That(field20.Value, Is.EqualTo(39));
        }

        [Test]
        public void Write()
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            DSetFieldDefinition definition = new DSetFieldDefinition();
            definition.DataTypeRaw = (byte)DSetFieldDefinition.FieldType.UInt32;
            definition.StoreParam1 = 1;
            definition.StoreParam2 = 20;
            definition.TableOffset = 0;

            strategy.WriteValue(writer, definition, 39);

            using var reader = new BinaryReader(stream);
            reader.BaseStream.Position = 0;

            Assert.That(reader.ReadUInt32(), Is.EqualTo(19));   // Base offset = 20, so subtract that.
        }
    }
}
