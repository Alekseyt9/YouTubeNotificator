using Microsoft.Extensions.Configuration;
using Xunit;
using YouTubeNotificator.Domain.Sevices;
using YouTubeNotificator.WebAPI.Service;

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
            IYouTubeService ytServ = new YouTubeServiceImpl();
            var result = await ytServ.GetChannelId("https://www.youtube.com/c/NERVOZZ");
            Assert.Equal(result, "UCvK4azuNYmcJzngT38yPsdw");
        }

        [Fact]
        public async Task GetChannelVideoTest()
        {
            var key = Configuration["youtube_ApiKey"];
            /*
            IYouTubeService ytServ = new YouTubeServiceImpl();
            var result = await ytServ.GetChannelVideos();
            */
        }


    }
}
