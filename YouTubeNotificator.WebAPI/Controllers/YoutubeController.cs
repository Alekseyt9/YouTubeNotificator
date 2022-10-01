using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YouTubeNotificationBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YoutubeController : ControllerBase
    {

        public async Task GetAsync()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "",
                ApplicationName = "YoutubeNotificationBot"
            });

            var searchListRequest = youtubeService.Channels.List("snippet");
            searchListRequest.ForUsername = "";
            var searchListResponse = await searchListRequest.ExecuteAsync();
        }


    }
}
