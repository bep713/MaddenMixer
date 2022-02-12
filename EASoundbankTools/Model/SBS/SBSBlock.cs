using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASoundbankTools.Model
{
    public class SBSBlock
    {
        public EAAudioHeader Header { get; set; }
        public ulong Offset { get; set; }
    }
}
