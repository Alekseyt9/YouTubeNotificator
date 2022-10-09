using Google.Apis.Services;
using System.Net;
using System.Text.RegularExpressions;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using YouTubeNotificator.Domain.Model;

namespace YouTubeNotificator.Domain.Sevices
{
    public class YouTubeServiceImpl : IYouTubeService
    {
        private YouTubeService _service;

        public YouTubeServiceImpl(IConfiguration conf)
        {
            _service = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = conf["youtube_ApiKey"],
                ApplicationName = conf["youtube_ApplicationName"]
            });
        }

        public async Task<ICollection<YouTubeVideoDto>> GetChannelVideos(string channelId, DateTime timeFrom)
        {
            var searchVideosRequest = _service.Search.List(
                new Google.Apis.Util.Repeatable<string>(
                    new string[] { "snippet" }
                ));
            searchVideosRequest.ChannelId = channelId;
            searchVideosRequest.MaxResults = 50;
            searchVideosRequest.PublishedAfter = timeFrom;
            var response = await searchVideosRequest.ExecuteAsync();

            var res = new List<YouTubeVideoDto>();
            foreach (var item in response.Items)
            {
                res.Add(new YouTubeVideoDto()
                {
                    Name = item.Snippet.Title,
                    Url = item.Id.VideoId,
                    Date = item.Snippet.PublishedAt
                });
            }

            return res;
        }

        public async Task<string> GetChannelTitle(string channelId)
        {
            var request = _service.Channels.List(
                new Google.Apis.Util.Repeatable<string>(
                    new string[] { "snippet", "contentDetails" }
                ));

            request.Id = channelId;
            var response = await request.ExecuteAsync();
            return response.Items.First().Snippet.Title;
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
