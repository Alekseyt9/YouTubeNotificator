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

        public async Task<ICollection<UserChannel>> GetChannels(Guid userId)
        {
            return await _dbContext.Channels
                .Where(x => x.UserId == userId)
                .ToListAsync<UserChannel>();
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

        public async void AddVideo(ChannelVideo vid)
        {
            await _dbContext.AddAsync(vid, CancellationToken.None);
        }

        public async void Commit()
        {
            await _dbContext.SaveChangesAsync(true, CancellationToken.None);
        }
    }
}
