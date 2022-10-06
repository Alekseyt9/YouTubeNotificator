using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YouTubeNotificator.Domain;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Concole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            var bot = host.Services.GetRequiredService<ITelegramBot>();

            await host.RunAsync();

            /*
            var mediator = host.Services.GetRequiredService<IMediator>();
            mediator.Send();
            */
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
                        .AddTransient<INotificationProcessor, NotificationProcessor>()
                        .AddSingleton<IConfiguration>(configuration)
                        .RegisterDomain()
                );
        }
    }
}