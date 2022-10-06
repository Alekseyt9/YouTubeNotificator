
using MediatR;
using YouTubeNotificator.Domain.Entities;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    internal class ListChannelsCommandHandler :
        IRequestHandler<ListChannelsCommand, ICollection<UserChannel>>
    {
        public Task<ICollection<UserChannel>> Handle(ListChannelsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
