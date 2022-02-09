using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Thank you to JYelton for inspiration
// https://stackoverflow.com/a/55540909/15056468

namespace MaddenMixer
{
    public class LogEntry
    {
        public uint Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Text { get; set; }
    }
}
