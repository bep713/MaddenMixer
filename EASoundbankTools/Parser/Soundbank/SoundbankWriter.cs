using EASoundbankTools.Model;
using EASoundbankTools.Parser.SBR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASoundbankTools.Parser
{
    public class SoundbankWriter
    {
        public void WriteAndFixOffsets(ISoundbank soundbank, SoundbankEntry entry, string newContentsPath, string outputPath)
        {
            if (soundbank is Soundbank_SbrSbs)
            {
                WriteAndFixOffsets((Soundbank_SbrSbs)soundbank, entry, newContentsPath, outputPath);
            }
        }

        private void WriteAndFixOffsets(Soundbank_SbrSbs soundbank, SoundbankEntry entry, string newContentsPath, string outputPath)
        {
            string writePathSbr = outputPath + ".sbr";
            string writePathSbs = outputPath + ".sbs";

            /* Overwrite SBS */
            using var readStream = File.Open(soundbank.SbsPath, FileMode.Open, FileAccess.Read);
            using var writeStream = File.Open(writePathSbs, FileMode.OpenOrCreate, FileAccess.Write);
            using var reader = new BinaryReader(readStream);
            using var writer = new BinaryWriter(writeStream);

            // Keep everything the same up to the offset
            writer.Write(reader.ReadBytes((int)entry.Offset));

            // Set reader position to next song offset
            SoundbankEntry nextSongEntry = soundbank.Entries.Find(x => x.SongOffset == entry.SongOffset + 1);
            reader.BaseStream.Position = (long)nextSongEntry.Offset;

            // Write the new data, inserting at the correct position
            using var newDataStream = File.Open(newContentsPath, FileMode.Open);
            using var newDataReader = new BinaryReader(newDataStream);

            writer.Write(newDataReader.ReadBytes((int)newDataReader.BaseStream.Length));

            // Write everything after the song to replace from the original file
            writer.Write(reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position)));

            /* Overwrite SBR */
            using var sbrReadStream = File.Open(soundbank.SbrPath, FileMode.Open, FileAccess.Read);
            using var sbrWriteStream = File.Open(writePathSbr, FileMode.OpenOrCreate, FileAccess.Write);
            using var sbrReader = new BinaryReader(sbrReadStream);
            using var sbrWriter = new BinaryWriter(sbrWriteStream);

            sbrWriter.Write(sbrReader.ReadBytes((int)soundbank.SongTableStartPosition));

            var type = StoreTypeStrategyFactory.Create(soundbank.SongOffsetDefinition.StoreType);
            ulong newSongLengthDifference = (ulong)newDataStream.Length - (nextSongEntry.Offset - entry.Offset);

            foreach(var soundbankEntry in soundbank.Entries)
            {
                ulong valueToWrite = soundbankEntry.RawOffsetInSbr;

                if (soundbankEntry.RawOffsetInSbr > entry.RawOffsetInSbr)
                {
                    valueToWrite += newSongLengthDifference;
                }

                type.WriteValue(sbrWriter, soundbank.SongOffsetDefinition, valueToWrite);
            }

            long bytesWrote = sbrWriter.BaseStream.Position - soundbank.SongTableStartPosition;
            sbrReader.BaseStream.Position += bytesWrote;

            sbrWriter.Write(sbrReader.ReadBytes((int)(sbrReader.BaseStream.Length - sbrReader.BaseStream.Position)));
        }
    }
}
