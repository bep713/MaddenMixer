using EASoundbankTools.Model;
using EASoundbankTools.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASoundbankTools.Parser.SBS
{
    public class SBSParser
    {
        private SBSFile SBSFile;

        public SBSFile ParseFromEntries(string SbsPath, List<SoundbankEntry> Entries)
        {
            SBSFile = new SBSFile();

            using var stream = File.Open(SbsPath, FileMode.Open);
            using var reader = new EndiannessAwareBinaryReader(stream, EndiannessAwareBinaryReader.EndiannessType.Big);

            foreach(var entry in Entries)
            {
                EAAudioHeader header = ParseEntryHeader(entry, reader);

                SBSBlock block = new SBSBlock
                {
                    Header = header,
                    Offset = entry.Offset
                };

                SBSFile.Blocks.Add(block);
            }

            return SBSFile;
        }

        public EAAudioHeader ParseEntryHeader(SoundbankEntry Entry, BinaryReader Reader)
        {
            long previousOffset = Reader.BaseStream.Position;

            Reader.BaseStream.Position = (long)Entry.Offset;
            uint headerBlock1 = Reader.ReadUInt32();

            if (headerBlock1 == 0x4800000C)
            {
                // Magic - advance 4 more bytes
                headerBlock1 = Reader.ReadUInt32();
            }

            uint version = headerBlock1 >> 0x1C;                    // first 4 bits
            uint codec = (headerBlock1 >> 0x18) & 0xF;              // next 4 bits
            uint channelConfig = (headerBlock1 >> 0x12) & 0x3F;     // next 6 bits
            uint sampleRate = headerBlock1 & 0x3FFFF;               // next 18 bits

            uint headerBlock2 = Reader.ReadUInt32();

            uint type = (headerBlock2 >> 0x1E) & 0x3;               // first 2 bits
            uint loopFlag = (headerBlock2 >> 0x1B) & 0x1;           // next 1 bit
            uint numberOfSamples = headerBlock2 & 0x1FFFFFFF;       // next 29 bits

            EAAudioHeader header = new EAAudioHeader
            {
                VersionRaw = version,
                CodecRaw = codec,
                ChannelConfig = channelConfig,
                SampleRate = sampleRate,
                TypeRaw = type,
                LoopFlag = loopFlag,
                NumberOfSamples = numberOfSamples
            };

            Reader.BaseStream.Position = previousOffset;

            return header;
        }
    }
}
