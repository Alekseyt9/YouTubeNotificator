

using YouTubeNotificator.Domain.Entities;
using YouTubeNotificator.Domain.Model;

namespace YouTubeNotificator.Domain.Sevices.Impl
{
    internal class NotificationProcessor : INotificationProcessor
    {
        private IAppRepository _appRepository;
        private IYouTubeService _youTubeService;
        private INotificationFormatter _notificationFormatter;
        private ITelegramBot _telegramBot;

        public NotificationProcessor(
            IAppRepository appRepository,
            IYouTubeService youTubeService,
            INotificationFormatter notificationFormatter,
            ITelegramBot telegramBot
        )
        {
            _appRepository = appRepository ??
                  throw new ArgumentNullException(nameof(appRepository));
            _youTubeService = youTubeService ??
                              throw new ArgumentNullException(nameof(youTubeService));
            _notificationFormatter = notificationFormatter ??
                                     throw new ArgumentNullException(nameof(notificationFormatter));
            _telegramBot = telegramBot ?? throw new ArgumentNullException(nameof(telegramBot));
        }

        public async Task Process(Guid userId)
        {
            var data = new NotificationData();
            var channels = await _appRepository.GetChannels(userId);

            var user = await _appRepository.GetUserById(userId);
            if (user == null)
            { 
                throw new Exception("user not found");
            }

            foreach (var channel in channels)
            {
                var dbVideoLast = await _appRepository.GetVideoLast(channel.Id);
                var ytVideos = await _youTubeService.GetChannelVideos(
                    channel.YoutubeId, dbVideoLast.Date.Value);
                var tuple = new Tuple<UserChannel, ICollection<ChannelVideo>>(
                    channel, new List<ChannelVideo>());

                foreach (var videoDto in ytVideos)
                {
                    var video = new ChannelVideo()
                    {
                        Id = Guid.NewGuid(),
                        Name = videoDto.Name,
                        Url = videoDto.Url,
                        Date = videoDto.Date
                    };
                    tuple.Item2.Add(video);
                    await _appRepository.AddVideo(video);
                }

                if (tuple.Item2.Any())
                {
                    data.Data.Add(tuple);
                }
            }

            if (data.Data.Any())
            {
                var msg = _notificationFormatter.FormatMessage(data);
                await _telegramBot.SendMessage(user.TelegramId, msg);
            }

            await _appRepository.Commit();
        }

    }
}
