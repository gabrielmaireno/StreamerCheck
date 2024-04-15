using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using StreamerCheck.Interfaces;
using StreamerCheck.Models;

namespace StreamerCheck.Clients
{
    public class Piczel : IApiAcess
    {
         public string streamPlataform { get; set; }
        public string onlineStreamerFile { get; set; }

        public Piczel()
        {
            onlineStreamerFile = "./txt/onlinePiczelStreamer.txt";
            streamPlataform = "https://www.piczel.tv/watch/";

        }

        public async Task<HashSet<string>> GetStreamersOnline()
        {
            HashSet<string> streamerNames = new HashSet<string>();
            var client = SettingClient();

            using (HttpResponseMessage response = await client.GetAsync("https://piczel.tv/api/streams/"))
            {
                string stringResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var streamerObject = JsonSerializer.Deserialize<List<PiczelResponseModel>>(stringResponse);
                    foreach (var item in streamerObject)
                    {
                        //System.Console.WriteLine(item.name);
                        streamerNames.Add(item.slug ?? "");
                    }
                }

                return streamerNames;
            }


        }

        private HttpClient SettingClient()
        {

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:104.0) Gecko/20100101 Firefox/104.0");
            client.DefaultRequestHeaders.Add("Accept", "*/*");

            return client;

        }
    }
}

