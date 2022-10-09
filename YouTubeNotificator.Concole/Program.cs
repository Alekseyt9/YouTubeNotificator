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
using YouTubeNotificator.Persistence.Services;

namespace YouTubeNotificator.Concole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                DBInitializer.Init(ctx);
            }
            await Start(host);

            await host.RunAsync();
        }

        static async Task Start(IHost host)
        {
            var taskManager = host.Services.GetRequiredService<INotificationTaskManager>();
            await taskManager.Start();

            var bot = host.Services.GetRequiredService<ITelegramBot>();
            bot.ReceiveMessage += async (sender, args) =>
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
                await mediator.Send(cmd);
            };
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .AddJsonFile($"appsettings.json", true, true); ;
            var configuration = builder.Build();

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services
                        .AddTransient<INotificationFormatter, TelegramNotificationFormatter>()
                        .AddTransient<INotificationTaskManager, NotificationTaskManager>()
                        .AddTransient<ITelegramCommandFactory, TelegramCommandFactory>()
                        .AddTransient<ITelegramCommandParser, TelegramCommandParser>()
                        .AddSingleton<IConfiguration>(configuration)
                        .AddSingleton<INotificationTaskManager, NotificationTaskManager>()
                        .AddSingleton<IYouTubeService, YouTubeServiceImpl>()
                        .RegisterDomain()
                        .RegisterPersistence(configuration)
                        .AddQuartz(q =>
                        {
                            q.UseMicrosoftDependencyInjectionJobFactory();
                        })

                );
        }
    }
}