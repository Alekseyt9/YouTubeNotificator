using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using YouTubeNotificator.Domain;
using YouTubeNotificator.Domain.Commands;
using YouTubeNotificator.Domain.Sevices;
using YouTubeNotificator.Domain.Sevices.Impl;
using YouTubeNotificator.Domain.Sevices.Implementation;

namespace YouTubeNotificator.Concole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            Start(host);
            await host.RunAsync();
        }

        static void Start(IHost host)
        {
            //var taskManager = host.Services.GetRequiredService<INotificationTaskManager>();
            //taskManager.Start();

            var bot = host.Services.GetRequiredService<ITelegramBot>();
            bot.ReceiveMessage += (sender, args) =>
            {
                var factory = host.Services.GetRequiredService<ITelegramCommandFactory>();
                var parser = host.Services.GetRequiredService<ITelegramCommandParser>();
                var cmdInfo = parser.Parse(args.Message);
                if (cmdInfo == null)
                {
                    return;
                }

                var cmd = (TelegramCommandBase)factory.Create(cmdInfo);
                cmd.Context = new TelegramBotContext()
                {
                    TelegramChannelId = args.ChannelId
                };
                var mediator = host.Services.GetRequiredService<IMediator>();
                mediator.Send(cmd);
            };
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Program>();
            var configuration = builder.Build();

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services
                        .AddTransient<INotificator, TelegramNotificator>()
                        .AddTransient<INotificationTaskManager, NotificationTaskManager>()
                        .AddTransient<ITelegramCommandFactory, TelegramCommandFactory>()
                        .AddTransient<ITelegramCommandParser, TelegramCommandParser>()
                        .AddSingleton<IConfiguration>(configuration)
                        .AddSingleton<INotificationTaskManager, NotificationTaskManager>()
                        .RegisterDomain()
                        .RegisterPersistence()
                        .AddQuartz()
                );
        }
    }
}