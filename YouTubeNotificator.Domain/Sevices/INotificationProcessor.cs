namespace YouTubeNotificator.Domain.Sevices;

public interface INotificationProcessor
{
    Task Process(Guid userId);
}