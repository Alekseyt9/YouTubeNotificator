
using MediatR;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    /// <summary>
    /// Создает пользователя в базе и записывает ChannelId
    /// </summary>
    internal class AddCommandHandler : AsyncRequestHandler<AddChannelCommand>
    {
        private ITelegramBot _telegramBot;

        public AddCommandHandler(ITelegramBot telegramBot)
        {
            _telegramBot = telegramBot ?? throw new ArgumentNullException(nameof(telegramBot));
        }

        protected override Task Handle(AddChannelCommand request, CancellationToken cancellationToken)
        {
            _telegramBot.SendMessage(
                request.Context.TelegramChannelId, $"cmd:add pars:{request.ChannelUrl}");
            return Task.CompletedTask;
        }
    }
}
