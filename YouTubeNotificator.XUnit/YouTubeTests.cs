using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using Xunit;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.XUnit
{
    public class YouTubeTests
    {

        IConfiguration Configuration { get; set; }

        public YouTubeTests()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<YouTubeTests>();
            Configuration = builder.Build();
        }

        [Fact]
        public async Task GetChannelIdTest()
        {
            IYouTubeService ytServ = new YouTubeServiceImpl(Configuration);
            var result = await ytServ.GetChannelId("https://www.youtube.com/c/NERVOZZ");
            Assert.Equal(result, "UCvK4azuNYmcJzngT38yPsdw");
        }

        [Fact]
        public async Task GetChannelVideoTest()
        {
            var apiKey = Configuration["youtube_ApiKey"]; 
            var appName = Configuration["ApplicationName"];

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = appName
            });

            IYouTubeService ytServ = new YouTubeServiceImpl(Configuration);
            var chanId = await ytServ.GetChannelId("https://www.youtube.com/c/NERVOZZ");

            var searchVideosRequest = youtubeService.Search.List(
                new Google.Apis.Util.Repeatable<string>(
                    new string[] { "snippet" }
                ));

            searchVideosRequest.ChannelId = chanId;
            searchVideosRequest.PublishedAfter = new DateTime(2022, 9, 20);
            var searchVideosResponse = await searchVideosRequest.ExecuteAsync();

            Assert.NotNull(searchVideosResponse);
            Assert.Equal(true, searchVideosResponse.Items.Count > 0);
        }


    }
}
