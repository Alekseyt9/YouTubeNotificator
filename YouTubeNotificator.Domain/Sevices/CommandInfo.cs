

using YouTubeNotificator.Domain.Commands;

namespace YouTubeNotificator.Domain.Sevices
{
    public class CommandInfo
    {
        public string ChannelId { get; set; }
        public TelegramCommandKind Kind { get; set; }
        public ICollection<string> Params { get; set; } = new List<string>();
    }
}
