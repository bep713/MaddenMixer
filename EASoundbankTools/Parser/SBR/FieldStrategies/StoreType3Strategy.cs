using EASoundbankTools.Model.SBR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EASoundbankTools.Parser.SBR
{
    public class StoreType3Strategy : IStoreTypeStrategy
    {
        public DSetField ParseValue(BinaryReader reader, DSetFieldDefinition definition, int index)
        {
            var indexBits = definition.StoreParam1 * 0xFF;
            var valSize = (ulong)(definition.StoreParam1 >> 8) & 0xFF;
            var numValues = definition.StoreParam2;
            var indexOffset = definition.TableOffset + numValues * valSize;

            var oldPosition = reader.BaseStream.Position;
            reader.BaseStream.Position = (long)indexOffset + (index * indexBits);

            var readByte = reader.ReadByte();
            var shift = (index * indexBits) % 8;
            readByte >>= shift;
            var newIndex = readByte & ((1 << indexBits) - 1);
            reader.BaseStream.Position = definition.TableOffset + (long)valSize * newIndex;

            ulong value;

            switch (valSize)
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

            DSetField field = new DSetField();
            field.Value = value;

            return field;
        }

        public void SetValue(DSetField field)
        {
            throw new NotImplementedException();
        }
    }
}
