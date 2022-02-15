using EASoundbankTools.Model.SBR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EASoundbankTools.Parser.SBR
{
    public class StoreType2Strategy : IStoreTypeStrategy
    {
        public DSetField ParseValue(BinaryReader reader, DSetFieldDefinition definition, int index)
        {
            var shift = definition.StoreParam1 & 0xFF;
            var valSize = (definition.StoreParam1 >> 8) & 0xFF;
            var baseOffset = definition.StoreParam2;

            var oldPosition = reader.BaseStream.Position;
            reader.BaseStream.Position = definition.TableOffset + (valSize * index);

            ulong value;

            switch(valSize)
            {
                default:
                case 1:
                    value = reader.ReadByte();
                    break;
                case 2:
                    value = reader.ReadUInt16();
                    break;
                case 4:
                    value = reader.ReadUInt32();
                    break;
                case 8:
                    value = reader.ReadUInt64();
                    break;
            }

            reader.BaseStream.Position = oldPosition;

            value <<= shift;
            value += baseOffset;

            DSetField field = new DSetField();
            field.Value = value;
            return field;
        }

        public void WriteValue(BinaryWriter writer, DSetFieldDefinition definition, ulong value)
        {
            var shift = definition.StoreParam1 & 0xFF;
            var valSize = (definition.StoreParam1 >> 8) & 0xFF;
            var baseOffset = definition.StoreParam2;

            value -= baseOffset;
            value >>= shift;

            switch (valSize)
            {
                case 1:
                    writer.Write((byte)value);
                    break;
                case 2:
                    writer.Write((ushort)value);
                    break;
                case 4:
                    writer.Write((uint)value);
                    break;
                case 8:
                    writer.Write(value);
                    break;
            }
        }
    }
}
