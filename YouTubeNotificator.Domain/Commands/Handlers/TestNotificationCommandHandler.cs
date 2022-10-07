using MediatR;
using YouTubeNotificator.Domain.Entities;
using YouTubeNotificator.Domain.Model;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    internal class TestNotificationCommandHandler : AsyncRequestHandler<TestNotificationCommand>
    {
        private INotificator _notificator;

        public TestNotificationCommandHandler(INotificator notificator)
        {
            _notificator = notificator ?? throw new ArgumentNullException(nameof(notificator));
        }

        protected override Task Handle(
            TestNotificationCommand request, 
            CancellationToken cancellationToken)
        {
            var userChannel = new UserChannel()
            {
                Name = "NERVOZ"
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

            _notificator.SendNotification(data);

            return Task.CompletedTask;
        }

    }
}
