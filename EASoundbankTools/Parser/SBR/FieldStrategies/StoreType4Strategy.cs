using EASoundbankTools.Model.SBR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EASoundbankTools.Parser.SBR
{
    public class StoreType4Strategy : IStoreTypeStrategy
    {
        public DSetField ParseValue(BinaryReader reader, DSetFieldDefinition definition, int index)
        {
            var oldPosition = reader.BaseStream.Position;
            reader.BaseStream.Position = definition.TableOffset + 8 * index;

            DSetField dSetField = new DSetField();
            dSetField.Value = reader.ReadUInt64();

            reader.BaseStream.Position = oldPosition;

            return dSetField;
        }

        public void WriteValue(BinaryWriter writer, DSetFieldDefinition definition, ulong value)
        {
            throw new NotImplementedException();
        }
    }
}
