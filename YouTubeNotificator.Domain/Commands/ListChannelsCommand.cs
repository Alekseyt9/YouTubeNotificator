

using MediatR;
using YouTubeNotificator.Domain.Entities;

namespace YouTubeNotificator.Domain.Commands
{
    internal class ListChannelsCommand : TelegramCommandBase, IRequest<ICollection<UserChannel>>
    {

    }
}
