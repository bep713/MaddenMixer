using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// This would not be possible without snrtool. Thank you!!
// https://github.com/jonwil/snrtool

namespace MaddenMixer
{
    public class MusicConverter
    {
        private static string snrtoolPath = Path.Combine(Directory.GetCurrentDirectory(), "Libs\\snrtool.exe");

        public static async Task<ConversionJob> ConvertMp3ToSbs(string mp3FilePath)
        {
            ConversionJob job = new ConversionJob
            {
                ProcessExePath = snrtoolPath,
                OutputPath = Path.Combine(Directory.GetCurrentDirectory(), "Temp\\conversion_result.sbs")
            };

            await job.ExecuteAsync("\"" + mp3FilePath + "\" \"" + job.OutputPath.Substring(0, job.OutputPath.Length - 4) + "\"");
            return job;
        }
    }
}
