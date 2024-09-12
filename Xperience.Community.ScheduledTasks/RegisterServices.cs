using CommandLine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static partial class RegisterServices
    {
        public static IServiceCollection AddScheduledTasks(this IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, ScheduledTasksStartupFilter>();
            return services;
        }
    }
}
