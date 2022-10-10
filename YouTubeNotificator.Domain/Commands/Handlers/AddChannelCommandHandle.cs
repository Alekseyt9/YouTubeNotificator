
using MediatR;
using YouTubeNotificator.Domain.Entities;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    /// <summary>
    /// Добавляет канал в базу
    /// </summary>
    internal class AddChannelCommandHandler : AsyncRequestHandler<AddChannelCommand>
    {
        private ITelegramBot _telegramBot;
        private IAppRepository _appRepository;
        private IYouTubeService _youTubeService;

        public AddChannelCommandHandler(
            ITelegramBot telegramBot, 
            IAppRepository appRepository,
            IYouTubeService youTubeService
        )
        {
            _telegramBot = telegramBot ?? 
                           throw new ArgumentNullException(nameof(telegramBot));
            _appRepository = appRepository ??
                             throw new ArgumentNullException(nameof(appRepository));
            _youTubeService = youTubeService ??
                              throw new ArgumentNullException(nameof(youTubeService));
        }

        protected override async Task Handle(AddChannelCommand request, CancellationToken cancellationToken)
        {
            await _telegramBot.SendMessage(
                request.Context.TelegramChannelId, $"cmd:add pars:{request.ChannelUrl}");

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

            try
            {
                var channelId = await _youTubeService.GetChannelId(request.ChannelUrl);
                var channelInfo = await _youTubeService.GetChannelInfo(channelId);

                var chan = new UserChannel()
                {
                    UserId = user.Id,
                    Id = Guid.NewGuid(),
                    User = user,
                    YoutubeId = channelId,
                    YoutubeName = channelInfo.Title,
                    YoutubeUrl = request.ChannelUrl,
                    PlaylistId = channelInfo.PlaylistId
                };
                await _appRepository.AddChannel(chan);
                await _appRepository.Commit();

                await _telegramBot.SendMessage(request.Context.TelegramChannelId, $"chanel added: {channelInfo.Title}");
            }
            catch (Exception e)
            {
                await _telegramBot.SendMessage(request.Context.TelegramChannelId, $"error: {e.Message}");
            }

        }
    }
}
