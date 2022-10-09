using YouTubeNotificator.Domain.Entities;
using YouTubeNotificator.Domain.Model;

namespace YouTubeNotificator.Domain.Sevices;

public interface IYouTubeService
{
    Task<ICollection<YouTubeVideoDto>> GetChannelVideos(
        string channelId, DateTime timeFrom);

    Task<string> GetChannelId(string url);

    Task<string> GetChannelTitle(string channelId);

}