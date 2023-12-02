using HBS.Xperience.TransformableViews.Repositories;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViews
{
    public class TransformableViewServices
    {
        public IServiceCollection AddTransformableView(IServiceCollection services)
        {
            services.AddSingleton<ITransformableViewRepository, TransformableViewRepository>();

            // Add the file provider
            services.Configure<MvcRazorRuntimeCompilationOptions>(opts =>
                opts.FileProviders.Add(
                    new TransformableViewFileProvider()
                )
            );
            return services;
        }
    }
}
