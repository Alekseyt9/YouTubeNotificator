
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Persistence.Services
{
    public class DBInitializer
    {

        public static async Task Init(AppDbContext ctx, IYouTubeService ytService)
        {
            ctx.Database.EnsureCreated();

            await InitNewFields(ctx, ytService);
        }


        private static async Task InitNewFields(AppDbContext ctx, IYouTubeService ytService)
        {
            string valNull = null;
            var channels =
                ctx.Channels.Where(x => x.PlaylistId == null);

            foreach (var chan in channels)
            {
                var chanInfo = await ytService.GetChannelInfo(chan.YoutubeId);
                chan.PlaylistId = chanInfo.PlaylistId;
            }

            await ctx.SaveChangesAsync();
        }

    }
}
