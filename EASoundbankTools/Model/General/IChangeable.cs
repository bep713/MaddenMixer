using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASoundbankTools.Model.General
{
    public interface IChangeable
    {
        public bool IsChanged { get; set; }
    }
}
