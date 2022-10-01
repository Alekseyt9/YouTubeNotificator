

namespace YouTubeNotificator.Domain.Sevices
{
    internal interface INotificationProcessor
    {
        Task Process(Guid userId);
    }
}
