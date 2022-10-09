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

        public async Task<ICollection<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync<User>();
        }

        public async Task<User> GetUserByTelegramId(long id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.TelegramId == id);
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

        public Task<UserChannel> GetChannel(Guid userId, string url)
        {
            throw new NotImplementedException();
        }

        public Task DelChannel(Guid userId, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ChannelVideo>> GetVideos(Guid chanId)
        {
            return await _dbContext.Videos
                .Where(x => x.ChannelId == chanId).ToListAsync();
        }

        public async Task<ChannelVideo> GetVideoLast(Guid chanId)
        {
            return await _dbContext.Videos
                .Where(x => x.ChannelId == chanId)
                .OrderBy(x => x.Date).LastOrDefaultAsync();
        }

        public async Task AddChannel(UserChannel chan)
        {
            await _dbContext.Channels.AddAsync(chan, CancellationToken.None);
        }

        public Task<User> GetUserById(Guid userId)
        {
            throw new NotImplementedException();
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
