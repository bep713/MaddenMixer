using System;
using System.Collections.Generic;
using System.IO;

// Thank you to NicknineTheEagle
// https://github.com/NicknineTheEagle/Frostbite-Scripts/blob/master/frostbite3/sbr.py

namespace EASoundbankTools.Model.SBR
{
    public class SBRFile
    {
        public enum SBREndianness : uint
        {
            Little = 0x53426C65,
            Big = 0x53426265
        };

        public enum SBRType : uint
        {
            Unknown = 0x0,
            Harmony = 0x4841524D,
            NewWaveResource = 0x7C7DF3AE
        };

        public SBRHeader Header { get; set; }
        public List<DSet> DSets { get; set; }

        public SBRFile()
        {
            Header = new SBRHeader();
            DSets = new List<DSet>();
        }
    }
}
