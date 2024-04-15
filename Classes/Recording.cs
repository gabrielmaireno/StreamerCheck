using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StreamerCheck.Clients;
using StreamerCheck.Interfaces;

namespace StreamerCheck.Classes
{
    public class Recording
    {
        IApiAcess client;

        string onlineStreamerFile;
        string recordingFile = "./txt/recording.txt";
        HashSet<string>? onlineStreamer;

        public Recording(IApiAcess apiAcess)
        {
            client = apiAcess;
            onlineStreamerFile = client.onlineStreamerFile;
        }

        void LookingStreamers(HashSet<string> streamerNames)
        {

            var streamerList = new HashSet<string>(File.ReadAllLines(@"./streamerList.txt"));


            //if (streamerList.Any(streamerNames.Contains))
            //{

            onlineStreamer = new HashSet<string>(streamerNames.Intersect(streamerList));
            File.WriteAllLines(onlineStreamerFile, onlineStreamer);
            //}


        }

        async Task ExecutingCmd(string streamer, HashSet<string> recording, HashSet<string> streamerOnline)
        {

            using (StreamWriter writer = File.AppendText(recordingFile))
            {

                await writer.WriteLineAsync(streamer);

            }

            recording.Add(streamer);

            System.Console.WriteLine($"current streamer: {streamer}");
            await CmdHandling.CmdStart(streamer, client.streamPlataform);

            //Thread.Sleep(3000);

            recording.Remove(streamer);

            using (StreamWriter sw = new StreamWriter(File.Open(recordingFile, FileMode.Create, FileAccess.Write, FileShare.Write)))
            {
                foreach (string item in recording)
                {
                    await sw.WriteLineAsync(item);
                }
            }

            System.Console.WriteLine("process finished");

        }

        public async Task StartRecording()
        {
            HashSet<string> streamersOnline = await client.GetStreamersOnline();
            LookingStreamers(streamersOnline);

            var recording = new HashSet<string>(File.ReadAllLines(recordingFile));
            var plataformOnlineStreamer = File.ReadAllLines(onlineStreamerFile);

            List<Task> recordingTasks = new List<Task>();

            foreach (var streamer in plataformOnlineStreamer)
            {

                // testing if any of the names inside recording file is one of the streamers on OnlineStreamerPlataform File
                if (!recording.Contains(streamer))
                {
                    //TODO : remake the logic inside here

                    Task recordingTask = ExecutingCmd(streamer, recording, streamersOnline);
                    recordingTasks.Add(recordingTask);

                }

            }

        }

    }
}