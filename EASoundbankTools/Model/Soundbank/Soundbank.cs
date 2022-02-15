using EASoundbankTools.Model.SBR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASoundbankTools.Model
{
    public abstract class Soundbank : ISoundbank
    {
        public string SbrPath { get; set; }
        public long SongTableStartPosition { get; set; }
        public SBRFile UnderlyingFile { get; set; }
        public List<SoundbankEntry> Entries { get; set; }
        public DSetFieldDefinition SongOffsetDefinition { get; set; }

        public Soundbank()
        {
            Entries = new List<SoundbankEntry>();
        }
    }
}
