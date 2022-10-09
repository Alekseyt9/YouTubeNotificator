
using YouTubeNotificator.Domain.Entities;

namespace YouTubeNotificator.Domain.Sevices
{
    public interface IAppRepository
    {

        Task<User> GetUserByTelegramId(long id);

        Task AddUser(User user); 

        Task<ICollection<UserChannel>> GetChannels(Guid userId);

        Task<UserChannel> GetChannel(Guid userId, string url);

        Task DelChannel(Guid userId, Guid id);

        Task<ICollection<ChannelVideo>> GetVideos(Guid chanId);

        Task<ChannelVideo> GetVideoLast(Guid chanId);

        Task AddVideo(ChannelVideo vid);

        Task Commit();

        Task AddChannel(UserChannel chan);

        Task<User> GetUserById(Guid userId);
    }
}
