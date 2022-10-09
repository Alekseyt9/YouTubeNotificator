
using MediatR;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    /// <summary>
    /// Удаляет канал из базы
    /// </summary>
    internal class DelChannelCommandHandler : AsyncRequestHandler<DelChannelCommand>
    {
        private ITelegramBot _telegramBot;
        private IAppRepository _appRepository;

        public DelChannelCommandHandler(
            ITelegramBot telegramBot,
            IAppRepository appRepository
            )
        {
            _telegramBot = telegramBot ?? 
                           throw new ArgumentNullException(nameof(telegramBot));
            _appRepository = appRepository ?? 
                             throw new ArgumentNullException(nameof(appRepository));
        }

        protected override async Task Handle(DelChannelCommand request, CancellationToken cancellationToken)
        {
            await _telegramBot.SendMessage(
                request.Context.TelegramChannelId, $"cmd:del pars:{request.ChannelUrl}");

            if (string.IsNullOrEmpty(request.ChannelUrl))
            {
                await _telegramBot.SendMessage(
                    request.Context.TelegramChannelId, $"parameter channelUrl not found");
                return;
            }

            var user = await _appRepository.GetUserByTelegramId(request.Context.TelegramChannelId);
            if (user == null)
            {
                await _telegramBot.SendMessage(request.Context.TelegramChannelId, "user not found");
                return;
            }

            var chan = await _appRepository.GetChannelByUrl(user.Id, request.ChannelUrl);
            if (chan == null)
            {
                await _telegramBot.SendMessage(request.Context.TelegramChannelId, "channel not found");
                return;
            }

            await _appRepository.DelChannel(chan);
            await _appRepository.Commit();

            await _telegramBot.SendMessage(request.Context.TelegramChannelId, "channel has been deleted");
        }

    }
}
