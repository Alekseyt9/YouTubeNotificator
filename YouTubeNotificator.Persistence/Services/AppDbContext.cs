using Microsoft.EntityFrameworkCore;
using YouTubeNotificator.Domain.Entities;
using YouTubeNotificator.Persistence.EntityConfigurations;

namespace YouTubeNotificator.Persistence.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserChannel> Channels { get; set; }

        public DbSet<ChannelVideo> Videos { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserChannelConfiguration());
            modelBuilder.ApplyConfiguration(new ChannelVideoConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        /*
        Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {

        }
        */

    }
}
