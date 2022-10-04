using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YouTubeNotificator.Domain.Sevices;
using YouTubeNotificator.WebAPI.Service;

namespace YouTubeNotificator.Concole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            //ExemplifyDisposableScoping(host.Services, "Scope 1");
            //Console.WriteLine();

            //ExemplifyDisposableScoping(host.Services, "Scope 2");
            //Console.WriteLine();

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>

                    services
                        .AddTransient<INotificator, TelegramNotificator>()
                        .AddTransient<INotificationProcessor, NotificationProcessor>()
                        //.AddScoped<ScopedDisposable>()
                        //.AddSingleton<SingletonDisposable>()

                );

    }
}