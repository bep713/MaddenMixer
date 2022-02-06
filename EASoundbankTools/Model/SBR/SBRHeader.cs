using System;
using System.Collections.Generic;
using System.Text;

namespace EASoundbankTools.Model.SBR
{
    public class SBRHeader
    {
        public uint Magic { get; set; }
        public ushort NumberOfDSets { get; set; }
        public uint SBRTypeRaw { get; set; }
        public uint TableOffset { get; set; }
        public uint DataOffset { get; set; }

        public SBRType SBRType
        {
            get
            {
                return (SBRType)SBRTypeRaw;
            }
        }
        public SBREndianness Endianness
        {
            get
            {
                return (SBREndianness)Magic;
            }
        }
    }
}
