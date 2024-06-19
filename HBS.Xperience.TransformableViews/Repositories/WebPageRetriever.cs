using CMS.ContentEngine;
using CMS.Websites;
using CMS.Websites.Routing;
using Kentico.Content.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViews.Repositories
{
    public class WebPageRetriever
    {
        private readonly IWebPageDataContextRetriever _contextRetriever;
        private readonly IContentQueryExecutor _queryExecutor;
        private readonly IWebsiteChannelContext _channelContext;

        public WebPageRetriever(IWebPageDataContextRetriever contextRetriever,
            IContentQueryExecutor queryExecutor, IWebsiteChannelContext channelContext)
        {
            _contextRetriever = contextRetriever;
            _queryExecutor = queryExecutor;
            _channelContext = channelContext;
        }

        internal async Task<T?> GetWebPage<T>(bool isAuthenticated) where T : class, IWebPageFieldsSource
        {
            var page = _contextRetriever.Retrieve().WebPage;

            var builder = new ContentItemQueryBuilder()
                .ForContentType(page.ContentTypeName,
                options => options
                .ForWebsite(page.WebsiteChannelName)
                .Where(x => x.WhereEquals(nameof(IWebPageContentQueryDataContainer.WebPageItemID), page.WebPageItemID))
                ).InLanguage(page.LanguageName);

            // Configures the query options for the query executor
            var queryOptions = new ContentQueryExecutionOptions()
            {
                ForPreview = _channelContext.IsPreview,
                IncludeSecuredItems = isAuthenticated || _channelContext.IsPreview
            };

            return (await _queryExecutor.GetMappedWebPageResult<T>(builder, queryOptions)).FirstOrDefault();
        }
    }
}
