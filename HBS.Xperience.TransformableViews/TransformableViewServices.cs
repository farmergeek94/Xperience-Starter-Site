using HBS.Xperience.TransformableViews.Repositories;
using HBS.Xperience.TransformableViews.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HBS.Xperience.TransformableViews
{
    public static class TransformableViewServices
    {
        public static IMvcBuilder AddTransformableViewsProvider(this IMvcBuilder builder, string aesKey)
        {
            builder.Services.AddSingleton<ITransformableViewRepository, TransformableViewRepository>();
            builder.Services.AddSingleton<IEncryptionService>(new EncryptionService(aesKey));

            builder.AddRazorRuntimeCompilation(cs => cs.FileProviders.Add(new TransformableViewFileProvider()));
            return builder;
        }
    }
}
