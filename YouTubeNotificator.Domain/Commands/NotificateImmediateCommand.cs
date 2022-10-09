
using MediatR;

namespace YouTubeNotificator.Domain.Commands
{
    internal class NotificateImmediateCommand : TelegramCommandBase, IRequest
    {
    }
}
