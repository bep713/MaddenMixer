using EASoundbankTools.Model;
using EASoundbankTools.Model.SBR;
using EASoundbankTools.Parser.SBR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASoundbankTools.Parser
{
    public class SoundbankParser
    {
        public ISoundbank ParseSbrSbs(string SbrPath, string SbsPath)
        {
            Soundbank_SbrSbs soundbank = new Soundbank_SbrSbs();
            soundbank.SbrPath = SbrPath;
            soundbank.SbsPath = SbsPath;
            soundbank.Entries = ParseSoundbankEntries(SbrPath);

            return soundbank;
        }

        public ISoundbank ParseSbrStandalone(string SbrPath)
        {
            Soundbank_SbrStandalone soundbank = new Soundbank_SbrStandalone();
            soundbank.SbrPath = SbrPath;
            soundbank.Entries = ParseSoundbankEntries(SbrPath);

            return soundbank;
        }

        private List<SoundbankEntry> ParseSoundbankEntries(string SbrPath)
        {
            List<SoundbankEntry> soundbankEntries = new List<SoundbankEntry>();
            SBRParser parser = new SBRParser();
            SBRFile file = parser.Parse(SbrPath);

            switch (file.Header.SBRType)
            {
                case SBRFile.SBRType.Harmony:
                    string fieldNameWithOffsets = "OFF";
                    ulong additionalValueAdjustment = 0;

                    if (IsSbrFileStandalone(file))
                    {
                        fieldNameWithOffsets = "RAM";
                        additionalValueAdjustment = file.DSets[0].DataOffset;
                    }

                    for (int i = 0; i < file.DSets[0].NumberOfElements; i++)
                    {
                        SoundbankEntry entry = new SoundbankEntry();
                        entry.RawOffsetInSbr = file.DSets[0].Records[i].GetFieldValueByName(fieldNameWithOffsets);
                        entry.Offset = entry.RawOffsetInSbr + additionalValueAdjustment;
                        entry.Name = i.ToString();

                        soundbankEntries.Add(entry);
                    }
                    break;
                case SBRFile.SBRType.NewWaveResource:
                    for (int i = 0; i < file.DSets[3].NumberOfElements; i++)
                    {
                        SoundbankEntry entry = new SoundbankEntry();
                        entry.RawOffsetInSbr = file.DSets[3].Records[i].Fields[2].Value;
                        entry.Offset = entry.RawOffsetInSbr - 3;
                        entry.Name = i.ToString();

                        soundbankEntries.Add(entry);
                    }
                    break;
            }

            return soundbankEntries;
        }

        public bool IsSbrFileStandalone(SBRFile file)
        {
            return file.DSets[0].Definitions.Find(x => x.Name == "OFF") == null;
        }
    }
}
