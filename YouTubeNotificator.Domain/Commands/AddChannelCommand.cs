using MediatR;

namespace YouTubeNotificator.Domain.Commands
{
    internal class AddChannelCommand : TelegramCommandBase, IRequest
    {
        public string ChannelUrl { get; set; }
    }
}
