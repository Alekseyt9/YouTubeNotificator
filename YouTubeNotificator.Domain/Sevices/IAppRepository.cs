
using YouTubeNotificator.Domain.Entities;

namespace YouTubeNotificator.Domain.Sevices
{
    public interface IAppRepository
    {
        Task<ICollection<User>> GetUsers();

        Task<ICollection<UserChannel>> GetChannels(Guid userId);

        Task<ICollection<ChannelVideo>> GetVideos(Guid chanId);

        Task<ChannelVideo> GetVideoLast(Guid chanId);

        void AddVideo(ChannelVideo vid);

        void Commit();
    }
}
