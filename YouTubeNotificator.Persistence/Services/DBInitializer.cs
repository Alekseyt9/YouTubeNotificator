
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Persistence.Services
{
    public class DBInitializer
    {

        public static async Task Init(AppDbContext ctx, IYouTubeService ytService)
        {
            await ctx.Database.EnsureCreatedAsync();
        }

    }
}
