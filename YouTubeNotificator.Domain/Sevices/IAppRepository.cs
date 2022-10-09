
using YouTubeNotificator.Domain.Entities;

namespace YouTubeNotificator.Domain.Sevices
{
    public interface IAppRepository
    {

        Task<User> GetUserByTelegramId(long id);

        Task AddUser(User user); 

        Task<ICollection<UserChannel>> GetChannels(Guid userId);

        Task<UserChannel> GetChannelByUrl(Guid userId, string url);

        Task DelChannel(UserChannel channel);

        Task<ICollection<ChannelVideo>> GetVideos(Guid chanId);

        Task<ChannelVideo> GetLastVideo(Guid chanId);

        Task AddVideo(ChannelVideo vid);

        Task Commit();

        Task AddChannel(UserChannel chan);

        Task<User> GetUserById(Guid userId);

        Task<IList<User>> GetUsers();
    }
}
