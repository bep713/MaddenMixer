using EASoundbankTools.Model.SBR;
using EASoundbankTools.Utility;
using System;
using System.IO;

namespace EASoundbankTools.Parser.SBR
{
    public class SBRParser
    {
        private SBRFile SBRFile;

        public SBRFile Parse(string FileName)
        {
            SBRFile = new SBRFile();

            using var stream = File.Open(FileName, FileMode.Open);
            using var reader = new EndiannessAwareBinaryReader(stream, EndiannessAwareBinaryReader.EndiannessType.Big);

            ParseSBRHeader(reader);

            for (int i = 0; i < SBRFile.Header.NumberOfDSets; i++)
            {
                reader.BaseStream.Position = SBRFile.Header.TableOffset + (i * 8);
                uint dSetOffset = reader.ReadUInt32();

                reader.BaseStream.Position = dSetOffset;
                ParseDSet(reader);
            }

            return SBRFile;
        }

        private void ParseSBRHeader(EndiannessAwareBinaryReader reader)
        {
            SBRFile.Header.Magic = reader.ReadUInt32();

            if (SBRFile.Header.Endianness == SBRFile.SBREndianness.Little)
            {
                reader.Endianness = EndiannessAwareBinaryReader.EndiannessType.Little;
            }
            else if (SBRFile.Header.Endianness != SBRFile.SBREndianness.Big)
            {
                throw new ArgumentException("The file passed in was not an SBR file.");
            }

            reader.BaseStream.Position = 0xA;
            SBRFile.Header.NumberOfDSets = reader.ReadUInt16();

            reader.BaseStream.Position = 0x10;
            SBRFile.Header.SBRTypeRaw = reader.ReadUInt32();

            reader.BaseStream.Position = 0x18;
            SBRFile.Header.TableOffset = reader.ReadUInt32();

            reader.BaseStream.Position = 0x20;
            SBRFile.Header.DataOffset = reader.ReadUInt32();
        }

        private void ParseDSet(EndiannessAwareBinaryReader reader)
        {
            DSet dSet = new DSet();

            dSet.Magic = reader.ReadUInt32();

            reader.BaseStream.Position += 0x4;
            dSet.Id = reader.ReadUInt32();

            reader.BaseStream.Position += 0xC;
            dSet.DataOffset = reader.ReadUInt32();

            reader.BaseStream.Position += 0x1C;
            dSet.NumberOfElements = reader.ReadUInt32();
            dSet.NumberOfFields = reader.ReadUInt16();

            reader.BaseStream.Position += 0xA;

            for (int i = 0; i < dSet.NumberOfFields; i++)
            {
                ParseFieldDefinition(reader, dSet);
            }

            for (int i = 0; i < dSet.NumberOfElements; i++)
            {
                ParseRecord(reader, dSet, i);
            }

            SBRFile.DSets.Add(dSet);
        }

        private void ParseFieldDefinition(EndiannessAwareBinaryReader reader, DSet dSet)
        {
            DSetFieldDefinition definition = new DSetFieldDefinition();

            definition.Id = reader.ReadUInt32();
            definition.DataTypeRaw = reader.ReadByte();
            definition.StoreType = reader.ReadByte();
            definition.StoreParam1 = reader.ReadUInt16();
            definition.StoreParam2 = reader.ReadUInt64();
            definition.TableOffset = reader.ReadUInt32();
            definition.Unknown = reader.ReadUInt32();

            dSet.Definitions.Add(definition);
        }

        private void ParseRecord(EndiannessAwareBinaryReader reader, DSet dSet, int index)
        {
            DSetRecord record = new DSetRecord();

            for (int i = 0; i < dSet.NumberOfFields; i++)
            {
                ParseField(reader, record, dSet.Definitions[i], index);
            }

            dSet.Records.Add(record);
        }

        private void ParseField(EndiannessAwareBinaryReader reader, DSetRecord record, DSetFieldDefinition definition, int index)
        {
            IStoreTypeStrategy strategy = StoreTypeStrategyFactory.Create(definition.StoreType);
            DSetField field = strategy.ParseValue(reader, definition, index);
            field.Name = definition.Name;

            record.Fields.Add(field);
        }
    }
}
