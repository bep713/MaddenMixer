using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaddenMixer
{
    public abstract class ProcessJob : IProcessJob
    {
        public string ProcessExePath { get; set; }
        public bool Status { get; set; }
        public List<string> StandardOutput { get; set; }
        public List<string> ErrorOutput { get; set; }

        public ProcessJob()
        {
            StandardOutput = new List<string>();
            ErrorOutput = new List<string>();
        }

        public async Task ExecuteAsync(string arguments)
        {
            using var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ProcessExePath,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    Arguments = arguments,
                }
            };

            proc.Start();
            await proc.WaitForExitAsync();

            StandardOutput = ReadLogStream(proc.StandardOutput);
            ErrorOutput = ReadLogStream(proc.StandardError);

            if (ErrorOutput.Count > 0)
            {
                Status = false;
            }
            else
            {
                Status = true;
            }
        }

        private List<string> ReadLogStream(StreamReader stream)
        {
            List<string> logs = new List<string>();

            while (!stream.EndOfStream)
            {
                logs.Add(stream.ReadLine());
            }

            return logs;
        }
    }
}
