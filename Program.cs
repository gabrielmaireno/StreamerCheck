using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using StreamerCheck.Classes;
using StreamerCheck.Clients;
using StreamerCheck.Interfaces;

class Program
{
    static async Task Main(string[] args)
    {
        Config.Yeet();
        Config.CheckConfigFile();

        Recording recTwitch = new Recording(new Twitch());
        Recording recPicarto = new Recording(new Picarto());
        Recording recPiczel = new Recording(new Piczel());
    
        

        while (true)
        {

            await recTwitch.StartRecording();
            await recPicarto.StartRecording();
            await recPiczel.StartRecording();
            Thread.Sleep(10000);

        }

    }

}


