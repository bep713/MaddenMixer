using EASoundbankTools.Model.SBR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EASoundbankTools.Parser.SBR
{
    public class StoreType1Strategy : IStoreTypeStrategy
    {
        public DSetField ParseValue(BinaryReader reader, DSetFieldDefinition definition, int index)
        {
            var delta = (ulong)definition.StoreParam1;
            var baseOffset = definition.StoreParam2;

            if ((delta & 0x8000) > 0)
            {
                delta -= 65536;
            }

            DSetField field = new DSetField();
            field.Value = baseOffset + delta * (uint)index;

            return field;
        }

        public void WriteValue(BinaryWriter writer, DSetFieldDefinition definition, ulong value)
        {
            writer.Write(value - definition.StoreParam2);
        }
    }
}
