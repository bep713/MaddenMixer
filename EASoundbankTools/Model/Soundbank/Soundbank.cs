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
        public List<SoundbankEntry> Entries { get; set; }

        public Soundbank()
        {
            Entries = new List<SoundbankEntry>();
        }
    }
}
