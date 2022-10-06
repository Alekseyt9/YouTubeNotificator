using MediatR;

namespace YouTubeNotificator.Domain.Commands
{
    internal class DelChannelCommand : TelegramCommandBase, IRequest
    {
        public string ChannelUrl { get; set; }
    }
}
