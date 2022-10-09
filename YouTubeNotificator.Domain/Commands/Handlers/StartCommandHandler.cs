
using MediatR;
using YouTubeNotificator.Domain.Entities;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    /// <summary>
    /// Создает пользователя в базе и записывает ChannelId
    /// </summary>
    internal class StartCommandHandler : AsyncRequestHandler<StartCommand>
    {
        private ITelegramBot _telegramBot;
        private IAppRepository _appRepository;

        public StartCommandHandler(ITelegramBot telegramBot, IAppRepository appRepository)
        {
            _telegramBot = telegramBot ?? throw new ArgumentNullException(nameof(telegramBot));
            _appRepository = appRepository ?? 
                             throw new ArgumentNullException(nameof(appRepository));
        }

        protected override async Task Handle(StartCommand request, CancellationToken cancellationToken)
        {
            await _telegramBot.SendMessage(request.Context.TelegramChannelId, "cmd:start");

            var user = await _appRepository.GetUserByTelegramId(request.Context.TelegramChannelId);
            if (user != null)
            {
                await _telegramBot.SendMessage(request.Context.TelegramChannelId, "user already exists");
                return;
            }

            user = new User()
            {
                Id = Guid.NewGuid(), 
                TelegramId = request.Context.TelegramChannelId
            };
            await _appRepository.AddUser(user);
            await _appRepository.Commit();

            await _telegramBot.SendMessage(request.Context.TelegramChannelId, "user created");
        }

    }
}
