
using MediatR;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    /// <summary>
    /// Обработать и уведомить немедленно
    /// </summary>
    internal class NotificateImmediateCommandHandler 
        : AsyncRequestHandler<NotificateImmediateCommand>
    {

        private ITelegramBot _telegramBot;
        private IAppRepository _appRepository;
        private INotificationProcessor _notificationProcessor;

        public NotificateImmediateCommandHandler(
            ITelegramBot telegramBot, 
            IAppRepository appRepository,
            INotificationProcessor notificationProcessor
        )
        {
            _telegramBot = telegramBot ?? 
                           throw new ArgumentNullException(nameof(telegramBot));
            _appRepository = appRepository ??
                             throw new ArgumentNullException(nameof(appRepository));
            _notificationProcessor = notificationProcessor ??
                                     throw new ArgumentNullException(nameof(notificationProcessor));
        }

        protected override async Task Handle(NotificateImmediateCommand request, CancellationToken cancellationToken)
        {
            await _telegramBot.SendMessage(request.Context.TelegramChannelId, "cmd:imm");

            var user = await _appRepository.GetUserByTelegramId(request.Context.TelegramChannelId);
            if (user == null)
            {
                await _telegramBot.SendMessage(request.Context.TelegramChannelId, "user not found");
                return;
            }

            await _notificationProcessor.Process(user.Id);
        }

    }
}
