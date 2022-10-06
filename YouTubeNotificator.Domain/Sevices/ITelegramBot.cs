
using YouTubeNotificator.Domain.Model;

namespace YouTubeNotificator.Domain.Sevices
{
    public interface ITelegramBot
    {
        event EventHandler<TelegramMessageEventArgs> ReceiveMessage;

        void SendMessage(long channelId, string msg);
    }
}
