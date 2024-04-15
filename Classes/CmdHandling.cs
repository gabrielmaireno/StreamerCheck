using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StreamerCheck.Classes
{

    public static class CmdHandling
    {
        static string programDirectory = Environment.CurrentDirectory;


        public static async Task CmdStart(string streamerName, string platformUrl)
        {

            await Task.Run(() =>
            {
                string command = FormattingCommandString(streamerName, platformUrl);

                using (Process process = new Process())
                {

                    process.StartInfo.WorkingDirectory = $@"{programDirectory}\TempStreams";
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = $"/c {command}";
                    process.StartInfo.CreateNoWindow = false;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    process.StartInfo.UseShellExecute = true;

                    process.Start();
                    process.WaitForExit();
                }
            });
        }
        static string FormattingCommandString(string streamerName, string platformUrl)
        {
            DateTime dateNow = DateTime.Now;
            string formattedDateNow = dateNow.ToString("yyyy.MM.dd.HH.mm.ss");

            string cmdCommand = $@"streamlink -o ""{streamerName}_{formattedDateNow}.ts"" ""{platformUrl}{streamerName}"" best";

            System.Console.WriteLine(cmdCommand);
            return cmdCommand;
        }
    }
}