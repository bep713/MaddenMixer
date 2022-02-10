using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This would not be possible without vgmstream. Thank you!!
// https://github.com/vgmstream/vgmstream

namespace MaddenMixer
{
    public class WavGenerator
    {
        private static string vgmPath = Path.Combine(Directory.GetCurrentDirectory(), "Libs/test.exe");

        public static async Task<WavGenerationJob> GenerateFromFileAsync(string filePath, int songIndex)
        {
            WavGenerationJob job = new WavGenerationJob
            {
                ProcessExePath = vgmPath,
                OutputPath = "test.wav"
            };

            await job.ExecuteAsync("-o " + job.OutputPath + " -s " + songIndex + " \"" + filePath + "\"");

            //using var proc = new Process
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        FileName = vgmPath,
            //        CreateNoWindow = true,
            //        UseShellExecute = false,
            //        RedirectStandardError = true,
            //        RedirectStandardOutput = true,
            //        Arguments = "-o " + job.OutputPath + " -s " + songIndex + " \"" + filePath + "\"",
            //    }
            //};

            //proc.Start();
            //await proc.WaitForExitAsync();

            //job.StandardOutput = ReadLogStream(proc.StandardOutput);
            //job.ErrorOutput = ReadLogStream(proc.StandardError);

            //if (job.ErrorOutput.Count > 0)
            //{
            //    job.Status = false;
            //}
            //else
            //{
            //    job.Status = true;
            //}

            return job;
        }
    }
}
