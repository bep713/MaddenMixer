using EASoundbankTools.Model;
using EASoundbankTools.Model.SBR;
using EASoundbankTools.Parser.SBR;
using EASoundbankTools.Parser.SBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASoundbankTools.Parser
{
    public class SoundbankParser
    {
        public Soundbank_SbrSbs ParseSbrSbs(string SbrPath, string SbsPath)
        {
            Soundbank_SbrSbs soundbank = new Soundbank_SbrSbs();
            soundbank.SbrPath = SbrPath;
            soundbank.SbsPath = SbsPath;
            soundbank.Entries = ParseSoundbankEntries(SbrPath);

            return soundbank;
        }

        public Soundbank_SbrStandalone ParseSbrStandalone(string SbrPath)
        {
            Soundbank_SbrStandalone soundbank = new Soundbank_SbrStandalone();
            soundbank.SbrPath = SbrPath;
            soundbank.Entries = ParseSoundbankEntries(SbrPath);
            AddEAAudioHeaderData(soundbank, SbrPath);

            return soundbank;
        }

        public ISoundbank ParseSbr(string SbrPath)
        {
            SBRParser parser = new SBRParser();
            SBRFile file = parser.Parse(SbrPath);
            ISoundbank soundbank;

            bool isStandalone = IsSbrFileStandalone(file);

            if (isStandalone)
            {
                soundbank = new Soundbank_SbrStandalone();
            }
            else
            {
                soundbank = new Soundbank_SbrSbs();
            }

            soundbank.SbrPath = SbrPath;
            soundbank.Entries = ParseSoundbankEntries(file);

            if (isStandalone)
            {
                AddEAAudioHeaderData(soundbank, SbrPath);
            }

            return soundbank;
        }

        public void AddEAAudioHeaderData(ISoundbank Soundbank, string SbsPath)
        {
            SBSParser parser = new SBSParser();
            SBSFile sbsFile = parser.ParseFromEntries(SbsPath, Soundbank.Entries);

            for (var i = 0; i < Soundbank.Entries.Count; i++)
            {
                Soundbank.Entries[i].Header = sbsFile.Blocks[i].Header;
            }
        }

        private List<SoundbankEntry> ParseSoundbankEntries(string SbrPath)
        {
            SBRParser parser = new SBRParser();
            SBRFile file = parser.Parse(SbrPath);

            return ParseSoundbankEntries(file);            
        }

        private List<SoundbankEntry> ParseSoundbankEntries(SBRFile file)
        {
            List<SoundbankEntry> soundbankEntries = new List<SoundbankEntry>();

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

            List<ulong> offsets = new List<ulong>();
            foreach(var entry in soundbankEntries)
            {
                if (offsets.FindIndex(x => x == entry.Offset) == -1)
                {
                    offsets.Add(entry.Offset);
                }
            }

            offsets.Sort();

            foreach(var entry in soundbankEntries)
            {
                entry.SongOffset = offsets.FindIndex(x => x == entry.Offset);
            }

            return soundbankEntries;
        }

        public bool IsSbrFileStandalone(SBRFile file)
        {
            return file.Header.SBRType != SBRFile.SBRType.NewWaveResource
                && file.DSets[0].Definitions.Find(x => x.Name == "OFF") == null;
        }
    }
}
