
namespace YouTubeNotificator.Domain.Model
{
    public class TelegramMessageEventArgs : EventArgs
    {
        public long ChannelId { get; set; }
        public string Message { get; set; }
    }
}
