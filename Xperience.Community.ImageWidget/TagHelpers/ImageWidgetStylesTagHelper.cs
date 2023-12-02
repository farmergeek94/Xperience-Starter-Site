﻿using CMS.Helpers;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xperience.Community.ImageWidget.TagHelpers
{

    public class ImageWidgetStylesTagHelper : TagHelper
    {
        public const string TagName = "image-widget-styles";

        private readonly IPageBuilderDataContextRetriever _pageBuilderDataContextRetriever;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;

        public ImageWidgetStylesTagHelper(IPageBuilderDataContextRetriever pageBuilderDataContextRetriever, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            _pageBuilderDataContextRetriever = pageBuilderDataContextRetriever;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_pageBuilderDataContextRetriever.Retrieve().EditMode) {
                var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
                output.TagMode = TagMode.SelfClosing;
                output.TagName = null;
                output.PostElement.AppendHtml(@$"<link rel='stylesheet' href='{urlHelper.Content("~/_content/Xperience.Community.ImageWidget/PageBuilder/Admin/InlineEditors/image-widget-image-selector/ImageSelector.css")}' />");
            } else
            {
                output.SuppressOutput();
            }
        }
    }
}
