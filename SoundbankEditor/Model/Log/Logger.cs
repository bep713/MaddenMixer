using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Thank you to JYelton for inspiration
// https://stackoverflow.com/a/55540909/15056468

namespace MaddenMixer
{
    public class Logger
    {
        private readonly Queue<LogEntry> log;
        private uint entryNumber = 0;
        private object logLock = new object();

        public Logger()
        {
            log = new Queue<LogEntry>();
        }

        public void AddToLog(string text)
        {
            lock (logLock)
            {
                var entry = new LogEntry
                {
                    Text = text,
                    Id = entryNumber,
                    Timestamp = DateTime.Now,
                };

                entryNumber++;
                log.Enqueue(entry);
            }
        }

        public void AddToLog(List<string> texts)
        {
            lock (logLock)
            {
                foreach (var text in texts)
                {
                    AddToLog(text);
                }
            }
        }

        public void Clear()
        {
            lock (logLock)
            {
                log.Clear();
            }
        }

        public string GetLogAsString()
        {
            lock (logLock)
            {
                var sb = new StringBuilder();
                //sb.AppendLine($@"{{\rtf1{{\colortbl;}}");

                foreach (var entry in log)
                {
                    sb.Append($"[{ entry.Timestamp.ToShortDateString() } { entry.Timestamp.ToShortTimeString() }] ");
                    sb.Append($"{ entry.Text }").AppendLine();
                }

                return sb.ToString();
            }
        }

        public List<string> GetLogAsStringList()
        {
            List<string> logList = new List<string>();

            lock (logLock)
            {
                foreach (var entry in log)
                {
                    logList.Add(entry.Text);
                }
            }

            return logList;
        }
    }
}
