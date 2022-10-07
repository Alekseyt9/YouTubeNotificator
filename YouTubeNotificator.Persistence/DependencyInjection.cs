using Microsoft.Extensions.DependencyInjection;
using YouTubeNotificator.Domain.Sevices;
using YouTubeNotificator.Persistence.Services;

namespace YouTubeNotificator.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPersistence(this IServiceCollection services)
        {
            services.AddSingleton<IAppRepository, AppRepository>();
            //services.AddDbContext<AppDbContext>();
            return services;
        }
    }
}
