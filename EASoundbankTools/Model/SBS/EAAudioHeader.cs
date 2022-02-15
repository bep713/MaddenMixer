using EASoundbankTools.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EASoundbankTools.Model.Enums;

namespace EASoundbankTools.Model
{
    public class EAAudioHeader
    {
        public uint VersionRaw { get; set; }
        public uint CodecRaw { get; set; }
        public uint ChannelConfig { get; set; }
        public uint SampleRate { get; set; }
        public uint TypeRaw { get; set; }
        public uint LoopFlag { get; set; }
        public uint NumberOfSamples { get; set; }

        public EAAudioVersion Version
        {
            get
            {
                return (EAAudioVersion)VersionRaw;
            }
        }

        public EAAudioCodec Codec
        {
            get 
            { 
                return (EAAudioCodec)CodecRaw; 
            }
        }

        public EAAudioType Type
        {
            get
            {
                return (EAAudioType)TypeRaw;
            }
        }

        public uint Channels
        {
            get
            {
                return ChannelConfig + 1;
            }
        }
    }
}
