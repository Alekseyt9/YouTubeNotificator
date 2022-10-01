using Microsoft.EntityFrameworkCore;
using YouTubeNotificator.Domain.Entities;

namespace YouTubeNotificator.Persistence.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserChannel> Channels { get; set; }

        public DbSet<ChannelVideo> Videos { get; set; }

        /*
        Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {

        }
        */

    }
}
