using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterSite.Logic.Library.TagHelpers
{
    [HtmlTargetElement(TagName)]
    public class RoutedPageTagHelper : TagHelper
    {
        public const string TagName = "routed-page";
        private readonly IWebPageDataContextRetriever _webPageDataContextRetriever;

        public bool Enabled { get; set; } = true;

        public RoutedPageTagHelper(IWebPageDataContextRetriever webPageDataContextRetriever)
        {
            _webPageDataContextRetriever = webPageDataContextRetriever;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            output.TagMode = TagMode.StartTagAndEndTag;
            var hasData = _webPageDataContextRetriever.TryRetrieve(out _);
            if ((Enabled && !hasData) || (!Enabled && hasData))
            {
                output.SuppressOutput();
            } 
            else
            {
                base.Process(context, output);
            }
        }
    }
}
