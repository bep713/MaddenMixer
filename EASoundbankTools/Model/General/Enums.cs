using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASoundbankTools.Model
{
    public class Enums
    {
        public enum EAAudioVersion
        {
            Version0 = 0,
            Version1 = 1
        };

        public enum EAAudioCodec
        {
            None = 0,
            Reserved = 1,
            PCM_16BE = 2,
            EA_XMA = 3,
            XAS1 = 4,
            EALayer3_V1 = 5,
            EALayer3_V2_PCM = 6,
            EALayer3_V2_Spike = 7,
            GCAD_PCM = 8,
            EASpeex = 9,
            EATrax = 10,
            EA_MP3 = 11,
            EA_Opus = 12,
            EA_Trac9 = 13,
            EA_OpusM = 14,
            EA_OpusMU = 15
        };

        public enum EAAudioType
        {
            RAM = 0,
            Stream = 1,
            GigaSample = 2
        };
    }
}
