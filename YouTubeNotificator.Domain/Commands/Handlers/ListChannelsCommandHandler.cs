
using System.Text;
using MediatR;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    /// <summary>
    /// Список каналов
    /// </summary>
    internal class ListChannelsCommandHandler : AsyncRequestHandler<ListChannelsCommand>
    {
        private ITelegramBot _telegramBot;
        private IAppRepository _appRepository;

        public ListChannelsCommandHandler(
            ITelegramBot telegramBot,
            IAppRepository appRepository
            )
        {
            _telegramBot = telegramBot ?? 
                           throw new ArgumentNullException(nameof(telegramBot));
            _appRepository = appRepository ??
                             throw new ArgumentNullException(nameof(appRepository));
        }

        protected override async Task Handle(ListChannelsCommand request, CancellationToken cancellationToken)
        {
            await _telegramBot.SendMessage(
                request.Context.TelegramChannelId, "cmd:ls");

            var user = await _appRepository.GetUserByTelegramId(request.Context.TelegramChannelId);
            if (user == null)
            {
                await _telegramBot.SendMessage(request.Context.TelegramChannelId, "user not found");
                return;
            }

            var channels = await _appRepository.GetChannels(user.Id);
            var sb = new StringBuilder();

            foreach (var chan in channels)
            {
                sb.AppendLine($"• <a href='{chan.YoutubeUrl}'>{chan.YoutubeName}</a> [{chan.YoutubeUrl}]");
            }

            await _telegramBot.SendMessage(request.Context.TelegramChannelId, sb.ToString());

        }
    }
}
