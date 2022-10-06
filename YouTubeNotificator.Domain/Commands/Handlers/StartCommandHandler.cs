
using MediatR;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    /// <summary>
    /// Создает пользователя в базе и записывает ChannelId
    /// </summary>
    internal class StartCommandHandler : AsyncRequestHandler<StartCommand>
    {

        protected override Task Handle(StartCommand request, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}
