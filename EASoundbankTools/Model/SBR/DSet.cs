using System;
using System.Collections.Generic;
using System.Text;

namespace EASoundbankTools.Model.SBR
{
    public class DSet
    {
        public uint Magic { get; set; }
        public uint Id { get; set; }
        public uint DataOffset { get; set; }
        public uint NumberOfElements { get; set; }
        public ushort NumberOfFields { get; set; }
        List<DSetField> Fields { get; set; }
    }
}
