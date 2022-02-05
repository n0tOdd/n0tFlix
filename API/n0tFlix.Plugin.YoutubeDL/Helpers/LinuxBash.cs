using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace n0tFlix.Plugin.YoutubeDL.Helpers
{
    public class LinuxBash
    {
        public static string GetCommandOutput(string args)
        {
            var escapedArgs = args.Replace("\"", "\\\"");

            ProcessStartInfo procStartInfo = new ProcessStartInfo("/bin/bash", $"-c \"{escapedArgs}\"")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = procStartInfo;
                process.Start();

                string result = process.StandardOutput.ReadToEnd();
                return (result);
            }
        }
    }
}