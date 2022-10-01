using YouTubeNotificator.Domain.Entities;

namespace YouTubeNotificator.Domain.Model
{
    public class NotificationData
    {
        public ICollection<Tuple<UserChannel, ICollection<ChannelVideo>>> Data { get; set; }
                = new List<Tuple<UserChannel, ICollection<ChannelVideo>>>();
    }
}
