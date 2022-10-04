
using YouTubeNotificator.Domain.Entities;
using YouTubeNotificator.Domain.Model;

namespace YouTubeNotificator.Domain.Sevices
{
    public class NotificationProcessor : INotificationProcessor
    {
        IAppRepository _appRepository;
        IYouTubeService _youTubeService;
        private INotificator _notificator;

        public NotificationProcessor(
            IAppRepository appRepository, IYouTubeService youTubeService, 
            INotificator notificator)
        {
            _appRepository = appRepository ?? 
                             throw new ArgumentNullException(nameof(appRepository));
            _youTubeService = youTubeService ?? 
                              throw new ArgumentNullException(nameof(youTubeService));
            _notificator = notificator ??
                           throw new ArgumentNullException(nameof(notificator));
        }

        public async Task Process(Guid userId)
        {
            var data = new NotificationData();
            var channels = await _appRepository.GetChannels(userId);

            foreach (var channel in channels)
            {
                var dbVideoLast = await _appRepository.GetVideoLast(channel.Id);
                var ytVideos = await _youTubeService.GetChannelVideos(
                    channel.YoutubeId, dbVideoLast.Date);
                var tuple = new Tuple<UserChannel, ICollection<ChannelVideo>>(
                    channel, new List<ChannelVideo>());

                foreach (var vid in ytVideos)
                {
                    tuple.Item2.Add(vid);
                    _appRepository.AddVideo(vid);
                }

                if (tuple.Item2.Any())
                {
                    data.Data.Add(tuple);
                }
            }

            if (data.Data.Any())
            {
                _notificator.SendNotification(data);
            }

            _appRepository.Commit();

        }

    }
}
