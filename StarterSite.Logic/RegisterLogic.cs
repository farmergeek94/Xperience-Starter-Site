﻿using Microsoft.Extensions.DependencyInjection;

namespace StarterSite.Logic
{
    public static class RegisterLogic
    {
        public static IServiceCollection AddLogic(this IServiceCollection services)
        {
            services.AddSingleton<IWebPageRepository, WebPageRepository>();
            services.AddSingleton<IPageBuilderRepository, PageBuilderRepository>();
            services.AddSingleton<INavigationRepository, NavigationRepository>();

            services.AddSingleton<ICacheScope, CacheScope>();
            services.AddSingleton<ICacheKeyService, CacheKeyService>();
            return services;
        }
    }
}
