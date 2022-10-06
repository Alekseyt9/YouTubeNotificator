
using MediatR;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    /// <summary>
    /// Создает пользователя в базе и записывает ChannelId
    /// </summary>
    internal class DelCommandHandler : AsyncRequestHandler<DelChannelCommand>
    {
        private ITelegramBot _telegramBot;

        public DelCommandHandler(ITelegramBot telegramBot)
        {
            _telegramBot = telegramBot ?? throw new ArgumentNullException(nameof(telegramBot));
        }

        protected override Task Handle(DelChannelCommand request, CancellationToken cancellationToken)
        {
            _telegramBot.SendMessage(
                request.Context.TelegramChannelId, $"cmd:del pars:{request.ChannelUrl}");
            return Task.CompletedTask;
        }
    }
}
