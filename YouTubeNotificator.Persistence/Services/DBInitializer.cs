

namespace YouTubeNotificator.Persistence.Services
{
    public class DBInitializer
    {
        public static void Init(AppDbContext ctx)
        {
            ctx.Database.EnsureCreated();
        }
    }
}
