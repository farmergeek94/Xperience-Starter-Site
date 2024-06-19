using CMS.ContentEngine;
using CMS.Helpers;
using CMS.Websites.Routing;
using HBS.Xperience.TransformableViews.Components;
using HBS.Xperience.TransformableViewsShared.Library;
using HBS.Xperience.TransformableViewsShared.Models;
using HBS.Xperience.TransformableViewsShared.Repositories;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViews.Components
{
    public class TransformableViewPageViewComponent : ViewComponent
    {
        private readonly IWebPageDataContextRetriever _contextRetriever;
        private readonly IContentQueryExecutor _queryExecutor;
        private readonly IWebsiteChannelContext _channelContext;

        public TransformableViewPageViewComponent(IWebPageDataContextRetriever contextRetriever,
            IContentQueryExecutor queryExecutor,
            IWebsiteChannelContext channelContext,
            IPreferredLanguageRetriever languageRetriever)
        {
            _contextRetriever = contextRetriever;
            _queryExecutor = queryExecutor;
            _channelContext = channelContext;
        }
        public IViewComponentResult Invoke(string view, dynamic model)
        {
            return View($"_TransformableView/{view}", model);
        }
    }
}
