using HBS.Xperience.TransformableViews.Repositories;
using HBS.Xperience.TransformableViewsShared.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HBS.Xperience.TransformableViews
{
    public static class TransformableViewServices
    {
        public static IMvcBuilder UseTransformableViewsProvider(this IMvcBuilder builder)
        {
            builder.AddRazorRuntimeCompilation(cs => cs.FileProviders.Insert(0, new TransformableViewFileProvider()));
            builder.Services.AddSingleton<ContentItemRetriever>();
            return builder;
        }
    }
}
