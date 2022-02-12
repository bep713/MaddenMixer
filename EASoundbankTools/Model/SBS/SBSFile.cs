using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASoundbankTools.Model
{
    public class SBSFile
    {
        public List<SBSBlock> Blocks { get; set; }

        public SBSFile()
        {
            Blocks = new List<SBSBlock>();
        }
    }
}
