using EASoundbankTools.Model.SBR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EASoundbankTools.Parser.SBR
{
    public class StoreType0Strategy : IStoreTypeStrategy
    {
        public DSetField ParseValue(BinaryReader reader, DSetFieldDefinition definition, int index)
        {
            var field = new DSetField();
            field.Value = definition.StoreParam2;

            return field;
        }

        public void WriteValue(BinaryWriter writer, DSetFieldDefinition definition, ulong value)
        {
            writer.Write(definition.StoreParam2);
        }
    }
}
