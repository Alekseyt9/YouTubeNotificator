
using MediatR;
using YouTubeNotificator.Domain.Entities;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    internal class ListChannelsCommandHandler : AsyncRequestHandler<ListChannelsCommand>
    {
        private ITelegramBot _telegramBot;

        public ListChannelsCommandHandler(ITelegramBot telegramBot)
        {
            _telegramBot = telegramBot ?? throw new ArgumentNullException(nameof(telegramBot));
        }

        protected override Task Handle(ListChannelsCommand request, CancellationToken cancellationToken)
        {
            _telegramBot.SendMessage(
                request.Context.TelegramChannelId, "cmd:ls");
            return Task.CompletedTask;
        }
    }
}
