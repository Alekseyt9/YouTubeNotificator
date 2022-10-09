using MediatR;
using YouTubeNotificator.Domain.Entities;
using YouTubeNotificator.Domain.Model;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    internal class TestNotificationCommandHandler : AsyncRequestHandler<TestNotificationCommand>
    {
        private INotificationFormatter _notificationFormatter;
        private ITelegramBot _telegramBot;

        public TestNotificationCommandHandler(
            INotificationFormatter notificationFormatter, 
            ITelegramBot telegramBot
            )
        {
            _notificationFormatter = notificationFormatter ?? throw new ArgumentNullException(nameof(notificationFormatter));
            _telegramBot = telegramBot ?? throw new ArgumentNullException(nameof(telegramBot));
        }

        protected override Task Handle(
            TestNotificationCommand request, 
            CancellationToken cancellationToken)
        {
            var userChannel = new UserChannel()
            {
                YoutubeName = "NERVOZ"
            };
            var vid1 = new ChannelVideo()
            {
                Date = DateTime.Now,
                Name = "ПРОЕКТ \"ЗАРАЖЕНИЕ\".ДОЖДЬ. ФИНАЛ.#ПОСТАПОКАЛИПСИС #ЗОМБИАПОКАЛИПСИС",
                Url = "https://www.youtube.com/watch?v=LMzquhXUrNo"
            };
            var vid2 = new ChannelVideo()
            {
                Date = DateTime.Now,
                Name = "В ОДНО МГНОВЕНИЕ. ФАНТАСТИЧЕСКИЙ РАССКАЗ.",
                Url = "https://www.youtube.com/watch?v=F_rTziSG07g"
            };
            var data = new NotificationData()
            {
                TelegramBotContext = request.Context
            };
            data.Data.Add(new Tuple<UserChannel, ICollection<ChannelVideo>>(
                    userChannel, new List<ChannelVideo>() {vid1, vid2}
                )
            );
            data.Data.Add(new Tuple<UserChannel, ICollection<ChannelVideo>>(
                    userChannel, new List<ChannelVideo>() { vid1, vid2 }
                )
            );

            var msg = _notificationFormatter.FormatMessage(data);
            _telegramBot.SendMessage(request.Context.TelegramChannelId, msg);

            return Task.CompletedTask;
        }

    }
}
