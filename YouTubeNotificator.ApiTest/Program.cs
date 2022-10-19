using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Net;
using System.Text.RegularExpressions;

namespace YouTubeApiTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("YouTube Data API: Search");
            Console.WriteLine("========================");

            try
            {
                new Program().Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private async Task Run()
        {
            
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDrdskvPeKSIxj7iEDO6n6hkkFISAQN2oA",
                ApplicationName = "YoutubeNotificationBot"
            });

            /*
            var searchChannelRequest = youtubeService.Channels.List(
                new Google.Apis.Util.Repeatable<string>(
                    new string[] { "snippet", "contentDetails" }
                ));

            searchChannelRequest.Id = "UCwHL6WHUarjGfUM_586me8w";
            //var searchChannelResponse = await searchChannelRequest.ExecuteAsync();
            */

            var chanId = await GetChannelId(
                "https://www.youtube.com/c/HighLoadChannel/videos");

            var searchVideosRequest = youtubeService.Search.List(
                new Google.Apis.Util.Repeatable<string>(
                    new string[] { "snippet" }
                ));
            searchVideosRequest.ChannelId = chanId;
            searchVideosRequest.MaxResults = 50;
            searchVideosRequest.PublishedAfter = new DateTime(2022, 9, 20);
            var searchVideosResponse = await searchVideosRequest.ExecuteAsync();
        }

        private async Task<string> GetChannelId(string url)
        {
            var urlData = await GetUrlData(url);
            return await ParseChannelId(urlData);
        }

        private async Task<string> ParseChannelId(string data)
        {
            string pat = @"""browseId"":""(.*?)""";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
            Match m = r.Match(data);
            if (m.Success)
            {
                return m.Groups[1].Value;
            }
            return null;
        }

        private async Task<string> GetUrlData(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

    }
}