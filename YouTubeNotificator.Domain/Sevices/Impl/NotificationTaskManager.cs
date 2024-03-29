﻿
using Quartz;
using YouTubeNotificator.Domain.Notification;
using User = YouTubeNotificator.Domain.Entities.User;

namespace YouTubeNotificator.Domain.Sevices
{
    public class NotificationTaskManager : INotificationTaskManager
    {
        private const string s_NotificationsGroup = "notifications";

        private ISchedulerFactory _schedulerFactory;
        private IAppRepository _appRepository;
        private IScheduler _scheduler;

        public NotificationTaskManager(
            ISchedulerFactory schedulerFactory, 
            IAppRepository appRepository)
        {
            _schedulerFactory = schedulerFactory ?? throw new ArgumentNullException(nameof(schedulerFactory));
            _appRepository = appRepository ?? throw new ArgumentNullException(nameof(appRepository));
        }

        public async Task Start()
        {
            _scheduler = await _schedulerFactory.GetScheduler();
            await _scheduler.Start();

            
            var users = await _appRepository.GetUsers();
            foreach (var user in users)
            {
                await CreateUserTask(user);
            }
            
        }

        private async Task CreateUserTask(User user)
        {
            var job = JobBuilder.Create<NotificationJob>()
                .WithIdentity(user.Id.ToString(), s_NotificationsGroup)
                .UsingJobData("userId", user.Id)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(user.Id.ToString(), s_NotificationsGroup)
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(30)
                    .RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(job, trigger);
        }

        public async Task AddUserTask(User user)
        {
            await CreateUserTask(user);
        }

    }
}
