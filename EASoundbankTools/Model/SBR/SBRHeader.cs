using System;
using System.Collections.Generic;
using System.Text;

// Thank you to NicknineTheEagle
// https://github.com/NicknineTheEagle/Frostbite-Scripts/blob/master/frostbite3/sbr.py

namespace EASoundbankTools.Model.SBR
{
    public class SBRHeader
    {
        public uint Magic { get; set; }
        public ushort NumberOfDSets { get; set; }
        public uint SBRTypeRaw { get; set; }
        public uint TableOffset { get; set; }
        public uint DataOffset { get; set; }

        public SBRFile.SBRType SBRType
        {
            get
            {
                return (SBRFile.SBRType)SBRTypeRaw;
            }
        }
        public SBRFile.SBREndianness Endianness
        {
            get
            {
                return (SBRFile.SBREndianness)Magic;
            }
        }
    }
}
