using EASoundbankTools.Model.SBR;
using EASoundbankTools.Utility;
using System.IO;

namespace EASoundbankTools.Parser.SBR
{
    public class SBRParser
    {
        public SBRFile SBRFile;

        public SBRFile Parse(string FileName)
        {
            SBRFile = new SBRFile();

            using var stream = File.Open(FileName, FileMode.Open);
            using var reader = new EndiannessAwareBinaryReader(stream, EndiannessAwareBinaryReader.EndiannessType.Big);

            SBRFile.Header.Magic = reader.ReadUInt32();
            
            if (SBRFile.Header.Endianness == SBREndianness.Little)
            {
                reader.Endianness = EndiannessAwareBinaryReader.EndiannessType.Little;
            }

            reader.BaseStream.Position = 0xA;
            SBRFile.Header.NumberOfDSets = reader.ReadUInt16();

            reader.BaseStream.Position = 0x10;
            SBRFile.Header.SBRTypeRaw = reader.ReadUInt32();

            reader.BaseStream.Position = 0x18;
            SBRFile.Header.TableOffset = reader.ReadUInt32();

            reader.BaseStream.Position = 0x20;
            SBRFile.Header.DataOffset = reader.ReadUInt32();

            for (int i = 0; i < SBRFile.Header.NumberOfDSets; i++)
            {

            }

            return SBRFile;
        }
    }
}
