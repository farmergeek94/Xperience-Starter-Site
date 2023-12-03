using HBS.Xperience.TransformableViews.Library;
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
    public static class TransformableViewServices
    {
        public static IMvcBuilder AddTransformableViewsProvider(this IMvcBuilder builder)
        {
            builder.Services.AddSingleton<ITransformableViewRepository, TransformableViewRepository>();

            builder.AddRazorRuntimeCompilation(cs => cs.FileProviders.Add(new TransformableViewFileProvider()));
            return builder;
        }
    }
}
