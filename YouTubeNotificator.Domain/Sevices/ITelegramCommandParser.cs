

using YouTubeNotificator.Domain.Model;

namespace YouTubeNotificator.Domain.Sevices
{
    public interface ITelegramCommandParser
    {
        CommandInfo Parse(string message);
    }
}
