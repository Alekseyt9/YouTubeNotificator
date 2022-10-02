using Xunit;
using YouTubeNotificator.Domain.Entities;
using YouTubeNotificator.Domain.Model;
using YouTubeNotificator.Domain.Sevices;
using YouTubeNotificator.WebAPI.Service;

namespace YouTubeNotificator.XUnit
{
    public class TelegramTests
    {
        [Fact]
        public void Test1()
        {
            INotificator telServ = new TelegramNotificator();

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
    }
}