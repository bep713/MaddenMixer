using System;
using System.Collections.Generic;
using System.Text;

// Thank you to NicknineTheEagle
// https://github.com/NicknineTheEagle/Frostbite-Scripts/blob/master/frostbite3/sbr.py

namespace EASoundbankTools.Model.SBR
{
    public class DSet
    {
        public uint Magic { get; set; }
        public uint Id { get; set; }
        public uint DataOffset { get; set; }
        public uint NumberOfElements { get; set; }
        public ushort NumberOfFields { get; set; }
        public List<DSetRecord> Records { get; set; }
        public List<DSetFieldDefinition> Definitions { get; set; }

        public DSet()
        {
            Records = new List<DSetRecord>();
            Definitions = new List<DSetFieldDefinition>();
        }
    }
}
