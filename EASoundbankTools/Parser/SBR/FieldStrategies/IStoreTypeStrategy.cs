using EASoundbankTools.Model.SBR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EASoundbankTools.Parser.SBR
{
    public interface IStoreTypeStrategy
    {
        public DSetField ParseValue(BinaryReader reader, DSetFieldDefinition definition, int index);
        public void WriteValue(BinaryWriter writer, DSetFieldDefinition definition, ulong value);
    }
}
