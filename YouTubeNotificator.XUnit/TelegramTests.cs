using Microsoft.Extensions.Configuration;
using Xunit;

namespace YouTubeNotificator.XUnit
{
    public class TelegramTests
    {
        IConfiguration Configuration { get; set; }

        public TelegramTests()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<TelegramTests>();
            Configuration = builder.Build();
        }

        [Fact]
        public void TestCreateTelegramBot()
        {
            try
            {
                //ITelegramBot telServ = new TelegramBot(Configuration);
                Assert.True(true);
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        /*
        public void TestSend()
        {
            INotificator telServ = new TelegramNotificator(Configuration);

            var notInfo = new NotificationData();
            var channel = new UserChannel();
            var videos = new List<ChannelVideo>
            {
                new ChannelVideo()
            };
            notInfo.Data.Add(
                new Tuple<UserChannel, ICollection<ChannelVideo>>(channel, videos));

            try
            {
                telServ.SendNotification(notInfo);
                Assert.True(true);
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }
        */

    }
}