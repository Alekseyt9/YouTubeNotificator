using Google.Apis.Services;
using System.Net;
using System.Text.RegularExpressions;
using Google.Apis.YouTube.v3;
using YouTubeNotificator.Domain.Entities;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Sevices
{
    public class YouTubeServiceImpl : IYouTubeService
    {

        public YouTubeServiceImpl()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Environment.GetEnvironmentVariable("youtube_ApiKey"),
                ApplicationName = Environment.GetEnvironmentVariable("youtube_ApplicationName")
            });
        }

        public Task<ICollection<ChannelVideo>> GetChannelVideos(string channelId, DateTime timeFrom)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetChannelId(string url)
        {
            var urlData = await GetUrlData(url);
            return await ParseChannelId(urlData);
        }

        private async Task<string> ParseChannelId(string data)
        {
            var pat = @"""browseId"":""(.*?)""";
            var r = new Regex(pat, RegexOptions.IgnoreCase);
            var m = r.Match(data);
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

            using (var response = (HttpWebResponse)await request.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

    }
}
