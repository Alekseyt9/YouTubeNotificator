using YouTubeNotificator.Domain.Entities;

namespace YouTubeNotificator.Domain.Sevices;

public interface IYouTubeService
{
    Task<ICollection<ChannelVideo>> GetChannelVideos(
        string channelId, DateTime timeFrom);

    Task<string> GetChannelId(string url);

}