
using MediatR;

namespace YouTubeNotificator.Domain.Commands
{
    internal class TestNotificationCommand : TelegramCommandBase, IRequest
    {
    }
}
