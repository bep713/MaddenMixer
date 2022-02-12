using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EASoundbankTools.Model.Enums;

namespace EASoundbankTools.Model
{
    public class SoundbankEntry
    {
        public string Name { get; set; }
        public ulong Offset { get; set; }
        public ulong RawOffsetInSbr { get; set; }
        public EAAudioHeader Header { get; set; }
        public EAAudioCodec Codec 
        { 
            get
            {
                return Header.Codec;
            }
        }
    }
}
