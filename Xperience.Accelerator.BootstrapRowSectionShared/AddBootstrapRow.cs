using Kentico.Xperience.Admin.Base.FormAnnotations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xperience.Accelerator.BootstrapRowSectionShared.Models;

namespace Xperience.Accelerator.BootstrapRowSectionShared
{
    public static class AddBootstrapRow
    {
        public static IServiceCollection AddBootstrapRowSection(this IServiceCollection services, Func<BootstrapRowOptions, BootstrapRowOptions>? options = null)
        {
            BootstrapRowOptions rowOptions;
            if (options == null)
            {
                var classes = new string[] {
                    "bg-primary"
                    , "bg-secondary"
                    , "bg-success"
                    , "bg-danger"
                    , "bg-warning"
                    , "bg-info"
                    , "bg-light"
                    , "bg-dark"
                    , "bg-body"
                    , "bg-white"
                    , "bg-transparent"
                };
                rowOptions = new BootstrapRowOptions
                {
                    BackgroundItems = classes.Select(x => new BootstrapOptionItem { Text = x, Value = x })
                };
            }
            else
            {
                rowOptions = options(new BootstrapRowOptions());
            }
            services.AddSingleton<IBootstrapRowOptions>(rowOptions);
            return services;
        }
    }

    public class BootstrapRowFormComponentAttribute : FormComponentAttribute
    {
    }
}