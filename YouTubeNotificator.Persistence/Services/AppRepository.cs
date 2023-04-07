
using Microsoft.EntityFrameworkCore;
using YouTubeNotificator.Domain.Entities;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Persistence.Services
{
    internal class AppRepository : IAppRepository
    {
        private AppDbContext _dbContext;

        public AppRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("dbContext");
        }

        public async Task<IList<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync<User>();
        }

        public async Task<User> GetUserByTelegramId(long id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.TelegramId == id);
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<ICollection<UserChannel>> GetChannels(Guid userId)
        {
            return await _dbContext.Channels
                .Where(x => x.UserId == userId)
                .ToListAsync<UserChannel>();
        }

        public async Task<UserChannel> GetChannelByUrl(Guid userId, string url)
        {
            return await _dbContext.Channels
                .FirstOrDefaultAsync(x => x.UserId == userId && x.YoutubeUrl == url);
        }

        public Task DelChannel(UserChannel channel)
        {
            _dbContext.Channels.Remove(channel);
            return Task.CompletedTask;
        }

        public async Task<ICollection<ChannelVideo>> GetVideos(Guid chanId)
        {
            return await _dbContext.Videos
                .Where(x => x.ChannelId == chanId).ToListAsync();
        }

        public async Task<ChannelVideo> GetLastVideo(Guid chanId)
        {
            return await _dbContext.Videos
                .Where(x => x.ChannelId == chanId)
                .OrderBy(x => x.Date).LastOrDefaultAsync();
        }

        public async Task AddChannel(UserChannel chan)
        {
            await _dbContext.Channels.AddAsync(chan, CancellationToken.None);
        }

        public async Task AddVideo(ChannelVideo vid)
        {
            await _dbContext.Videos.AddAsync(vid, CancellationToken.None);
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync(true, CancellationToken.None);
        }

    }
}
