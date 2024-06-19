using HBS.Xperience.TransformableViews.Repositories;
using HBS.Xperience.TransformableViewsShared.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HBS.Xperience.TransformableViews
{
    public static class TransformableViewServices
    {
        public static IMvcBuilder UseTransformableViewsProvider(this IMvcBuilder builder)
        {
            builder.AddRazorRuntimeCompilation(cs => cs.FileProviders.Add(new TransformableViewFileProvider()));
            builder.Services.AddSingleton<WebPageRetriever>();
            return builder;
        }
    }
}
