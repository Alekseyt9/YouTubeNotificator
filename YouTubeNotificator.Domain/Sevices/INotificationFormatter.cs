using YouTubeNotificator.Domain.Model;

namespace YouTubeNotificator.Domain.Sevices
{
    public interface INotificationFormatter
    {

        string FormatMessage(NotificationData data);

    }
}
