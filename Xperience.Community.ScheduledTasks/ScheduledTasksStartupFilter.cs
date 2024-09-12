using Microsoft.AspNetCore.Hosting;

namespace Microsoft.AspNetCore.Builder
{
    public static partial class RegisterServices
    {
        public class ScheduledTasksStartupFilter() : IStartupFilter
        {
            public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            {
                return builder =>
                {
                    next(builder);
                };
            }
        }
    }
}
