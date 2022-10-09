
using Quartz;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Notification
{
    internal class NotificationJob : IJob
    {
        private INotificationProcessor _notificationProcessor;

        public NotificationJob(INotificationProcessor notificationProcessor)
        {
            _notificationProcessor = notificationProcessor ??
                                     throw new ArgumentNullException(nameof(notificationProcessor));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var userId = context.JobDetail.JobDataMap.GetGuidValue("userId");
            await _notificationProcessor.Process(userId);
        }

    }
}
