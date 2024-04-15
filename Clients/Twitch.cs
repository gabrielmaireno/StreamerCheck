using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using StreamerCheck.Interfaces;
using StreamerCheck.Models;
using StreamerCheck.Classes;


namespace StreamerCheck.Clients
{

    public class Twitch : IApiAcess
    {

        string twitchCategoryUrl = "https://api.twitch.tv/helix/streams?game_id=509660&first=100";

        string clientID = Config.configValues[0];
        string clientSecret = Config.configValues[1];
        string accessToken = "";
        public string streamPlataform { get; set; }
        public string onlineStreamerFile { get; set; }

        public Twitch()
        {


            onlineStreamerFile = "./txt/onlineTwitchStreamer.txt";
            streamPlataform = "https://www.twitch.tv/";
            PostTwitchAcessAsync().Wait(); //take first token
            Thread refresh = new Thread(RefreshAcessToken); //runs the PostTwitchAcessAsync in x amount of time indefinitely, but in a new thread
            refresh.Start();

        }

        public async Task<HashSet<string>> GetStreamersOnline()
        {
            HashSet<string> streamerNames = new HashSet<string>();
            var client = SettingClient();

            //setting new client headers fo get request
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            client.DefaultRequestHeaders.Add("Client-ID", $"{clientID}");

            using (HttpResponseMessage response = await client.GetAsync(twitchCategoryUrl))
            {
                string stringResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var streamerObject = JsonSerializer.Deserialize<TwitchResponseJsonModel>(stringResponse);
                    //System.Console.WriteLine(stringResponse);

                    foreach (var StreamInfo in streamerObject.data)
                    {
                        //System.Console.WriteLine(StreamInfo.user_login);
                        streamerNames.Add(StreamInfo.user_login ?? "");
                    }

                    string? nextCursor = streamerObject.pagination.cursor;

                    while (nextCursor != null)
                    {

                        using (HttpResponseMessage nextResponse = await client.GetAsync($"{twitchCategoryUrl}&after={nextCursor}"))
                        {
                            string nextStringResponse = await nextResponse.Content.ReadAsStringAsync();

                            if (nextResponse.IsSuccessStatusCode)
                            {
                                var nextStreamerObject = JsonSerializer.Deserialize<TwitchResponseJsonModel>(nextStringResponse);

                                foreach (var StreamInfo in nextStreamerObject.data)
                                {
                                    //System.Console.WriteLine(StreamInfo.user_login);
                                    streamerNames.Add(StreamInfo.user_login ?? "");
                                }
                                nextCursor = nextStreamerObject.pagination.cursor;
                                //System.Console.WriteLine(nextCursor);
                            }
                        }
                    }
                    return streamerNames;
                }
                return streamerNames;
            }
        }

        async Task PostTwitchAcessAsync()
        {
            var client = SettingClient();


            var postData = new Dictionary<string, string>
            {
                {"client_id", clientID},
                {"client_secret", clientSecret},
                {"grant_type", "client_credentials"}
            };

            using (HttpResponseMessage response = await client.PostAsync("https://id.twitch.tv/oauth2/token", new FormUrlEncodedContent(postData)))
            {

                string readanbleContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(readanbleContent);
                    accessToken = responseObject["access_token"].GetString() ?? "";
                    System.Console.WriteLine(accessToken);
                }
            }
        }

        private HttpClient SettingClient()
        {

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:104.0) Gecko/20100101 Firefox/104.0");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

            return client;

        }

        private void RefreshAcessToken()
        {
            int msConversionToMinutes = 60000;
            int minutes = 55;

            while (accessToken != null)
            {
                Thread.Sleep(msConversionToMinutes * minutes);
                PostTwitchAcessAsync().Wait();
            }

        }
    }
}