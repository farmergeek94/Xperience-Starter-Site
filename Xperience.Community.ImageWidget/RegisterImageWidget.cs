using Microsoft.Extensions.DependencyInjection;
using Xperience.Community.ImageWidget.Context;
using Xperience.Community.ImageWidget.Repositories.Implementations;
using Xperience.Community.ImageWidget.Repositories.Interfaces;

namespace Xperience.Community.ImageWidget
{
    public static class RegisterImageWidget
    {
        public static IServiceCollection AddImageWidget(this IServiceCollection services)
        {
            services.AddSingleton<IMediaFileRepository, MediaFileRepository>();
            services.AddSingleton<ICacheScope, CacheScope>();
            return services;
        }
    }
}
