using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterSite.RCL
{
    public static class FeatureFolders
    {
        public static void AddFeatureFolders(this IList<string> viewLocationFormats)
        {
            // Add Feature Folders
            viewLocationFormats.Add("/Features/{1}/{0}.cshtml");
            viewLocationFormats.Add("/Features/Shared/{0}.cshtml");

            // Custom KM
            // Adds /Components/PageTypes/{ComponentName}/{ComponentViewName}.cshtml
            //viewLocationFormats.Add("/Components/InlineEditors/{0}.cshtml");
            //viewLocationFormats.Add("/Components/Navigation/{0}.cshtml");
            viewLocationFormats.Add("/Components/Widgets/{0}.cshtml");

            // Some components are in Features if they are a page template
            viewLocationFormats.Add("/Features/{0}.cshtml");

            // Paths for my Custom Structure, leveraged with the _CustomViewPath and _CustomController values set in PopulateValues
            // Handles Basic Widgets/Sections/PageTemplates
            //viewLocationFormats.Add("/{0}.cshtml");
        }
    }
}
