using YouTubeNotificator.Domain.Model;

namespace YouTubeNotificator.Domain.Sevices
{
    public interface INotificator
    {
        void SendNotification(NotificationData data);
    }
}
