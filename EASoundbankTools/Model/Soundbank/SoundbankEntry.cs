using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASoundbankTools.Model
{
    public class SoundbankEntry
    {
        public string Name { get; set; }
        public ulong Offset { get; set; }
        public ulong RawOffsetInSbr { get; set; }
    }
}
