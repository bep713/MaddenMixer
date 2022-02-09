using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaddenMixer
{
    public interface IProcessJob : IJob
    {
        public List<string> StandardOutput { get; set; }
        public List<string> ErrorOutput { get; set; }
    }
}
