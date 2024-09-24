using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Xperience.Community.BootstrapRowSection.TagHelpers
{
    [HtmlTargetElement(TagName)]
    public class ContainerTagHelper : TagHelper
    {
        public const string TagName = "container";

        public string Class { get; set; } = "";

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            if(!string.IsNullOrWhiteSpace(Class) )
            {
                output.TagName = "div";
                output.AddClass(Class, HtmlEncoder.Default);
            } else
            {
                output.TagName = null;
            }
            return base.ProcessAsync(context, output);
        }
    }
}
