using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaddenMixer
{
    public abstract class ProcessJob : IProcessJob
    {
        public bool Status { get; set; }
        public List<string> StandardOutput { get; set; }
        public List<string> ErrorOutput { get; set; }

        public ProcessJob()
        {
            StandardOutput = new List<string>();
            ErrorOutput = new List<string>();
        }
    }
}
