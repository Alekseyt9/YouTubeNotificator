﻿using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YouTubeNotificator.Domain.Notification;
using YouTubeNotificator.Domain.Sevices;
using YouTubeNotificator.Domain.Sevices.Impl;

namespace YouTubeNotificator.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDomain(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<ITelegramBot, TelegramBot>();
            services.AddTransient<INotificationProcessor, NotificationProcessor>();
            services.AddTransient<NotificationJob>();
            return services;
        }
    }
}
