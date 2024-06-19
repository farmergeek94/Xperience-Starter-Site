using CMS.ContentEngine;
using CMS.DataEngine;
using CMS.Websites;
using CMS.Websites.Routing;
using HBS.TransformableViews_Experience;
using HBS.Xperience.TransformableViews.PageTemplates;
using HBS.Xperience.TransformableViews.Repositories;
using HBS.Xperience.TransformableViewsShared.Library;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc.PageTemplates;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: RegisterPageTemplate("HBS.TransformableViewPageTemplate", "Transformable View Template", typeof(TransformableViewPageTemplateProperties), "~/PageTemplates/_TransformableViewPageTemplate.cshtml", Description = "Page Template for using the transformable views")]

namespace HBS.Xperience.TransformableViews.PageTemplates
{
    public class TransformableViewController<T> : Controller where T : class, IWebPageFieldsSource
    {
        private readonly WebPageRetriever _webPageRetriever;

        public TransformableViewController(WebPageRetriever webPageRetriever)
        {
            _webPageRetriever = webPageRetriever;
        }

        public async Task<IActionResult> Index()
        {
            return new TemplateResult(await _webPageRetriever.GetWebPage<T>(User.Identity?.IsAuthenticated ?? false));
        }
    }

    public class TransformableViewPageTemplateProperties : IPageTemplateProperties
    {
        [ObjectSelectorComponent(TransformableViewInfo.OBJECT_TYPE, WhereConditionProviderType = typeof(TransformableViewPageWhere), OrderBy = new string[] { nameof(TransformableViewInfo.TransformableViewDisplayName) })]
        public string View { get; set; } = string.Empty;
    }

    internal class TransformableViewPageWhere : IObjectSelectorWhereConditionProvider
    {
        // Where condition limiting the selectable objects
        public WhereCondition Get() => new WhereCondition().WhereEquals(nameof(TransformableViewInfo.TransformableViewType), (int)TransformableViewTypeEnum.Page);
    }
}
