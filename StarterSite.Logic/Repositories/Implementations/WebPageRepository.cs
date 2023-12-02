using CMS.ContentEngine;
using CMS.Websites;
using CMS.Websites.Routing;
using StarterSite.Logic.Context;
using StarterSite.Models;
using StarterSite.Logic.Repositories.Interfaces;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Helpers;
using StarterSite.Logic.Library;
using CMS.Base.Internal;
using Microsoft.AspNetCore.Http;

namespace StarterSite.Logic.Repositories.Implementations
{
    public class WebPageRepository : IWebPageRepository
    {
        private readonly IWebsiteChannelContext _websiteChannelContext;
        private readonly IContentQueryExecutor _executor;
        private readonly IWebPageQueryResultMapper _mapper;
        private readonly IWebPageUrlRetriever _urlRetriever;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IProgressiveCache _progressiveCache;
        private readonly IPageBuilderRepository _pageBuilderRepository;

        public WebPageRepository(IWebsiteChannelContext websiteChannelContext
            , IContentQueryExecutor executor
            , IWebPageQueryResultMapper mapper
            , IWebPageUrlRetriever urlRetriever
            , ICacheKeyService cacheKeyService
            , IProgressiveCache progressiveCache
            , IPageBuilderRepository pageBuilderRepository)
        {
            _websiteChannelContext = websiteChannelContext;
            _executor = executor;
            _mapper = mapper;
            _urlRetriever = urlRetriever;
            _cacheKeyService = cacheKeyService;
            _progressiveCache = progressiveCache;
            _pageBuilderRepository = pageBuilderRepository;
        }

        public async Task<WebPageItem> GetPageItem<T>(int id, string language = "en") where T : IWebPageFieldsSource, new()
        {
            var cacheKeys = _cacheKeyService.Create();
            cacheKeys.Page(id);

            // Get the page type from the type passed in. 
            string pageType = ((string?)typeof(T).GetField("CONTENT_TYPE_NAME")?.GetValue(null)) ?? "";

            var builder = new ContentItemQueryBuilder()
                        .ForContentType(
                            // Scopes the query to pages of the 'My.ArticlePage' content type
                            pageType,
                            config => config
                                // Retrieves pages only from the specified channel
                                .Where(x => x.WhereEquals(nameof(IWebPageContentQueryDataContainer.WebPageItemID), id))
                                .Columns("WebPageItemGUID", "WebPageItemID", "WebPageItemName", "WebPageItemOrder", "WebPageItemParentID", "WebPageItemTreePath", "WebPageUrlPath")
                                .ForWebsite(_websiteChannelContext.WebsiteChannelName)
                        // Retrieves only English variants of pages
                        ).InLanguage(language);

            // Executes the query and stores the data in generated 'ArticlePage' models
            IEnumerable<T> pages = await _progressiveCache.LoadAsync(async cs =>
            {
                if (cs.Cached)
                {
                    cs.CacheDependency = cacheKeys.GetCMSCacheDependency();
                }
                return await _executor.GetWebPageResult(
                                                        builder: builder,
                                                        resultSelector: _mapper.Map<T>);
            }, new CacheSettings(CacheTiming.VeryLong, "GetPageItem", language, id, pageType));

            var page = pages.FirstOrDefault();
            
            return page != null ? new WebPageItem
            {
                WebPageItemGUID = page.SystemFields.WebPageItemGUID,
                WebPageItemID = page.SystemFields.WebPageItemID,
                WebPageItemName = page.SystemFields.WebPageItemTreePath.Split("/").Last(),
                WebPageItemOrder = page.SystemFields.WebPageItemOrder,
                WebPageItemParentID = page.SystemFields.WebPageItemParentID,
                WebPageItemTreePath = page.SystemFields.WebPageItemTreePath,
                WebPageUrlPath = page.SystemFields.WebPageUrlPath,
                WebPageRelativeUrl = (await _urlRetriever.Retrieve(page, language)).RelativePath
            } : new WebPageItem();
        }

        public async Task<WebPageItem<TI>> GetCurrentPageItem<T, TI>(Func<T, TI> dataMapper) where T : IWebPageFieldsSource, new() where TI : new()
        {
            return await GetPageItem(_pageBuilderRepository.PageContext.WebPageItemID, dataMapper, _pageBuilderRepository.Language, _pageBuilderRepository.IsPreview);
        }

        public async Task<WebPageItem<TI>> GetPageItem<T, TI>(int id, Func<T, TI> dataMapper, string language = "en", bool forPreview = false) where T : IWebPageFieldsSource, new() where TI : new()
        {
            var cacheKeys = _cacheKeyService.Create();
            cacheKeys.Page(id);

            // Get the page type from the type passed in. 
            string pageType = ((string?)typeof(T).GetField("CONTENT_TYPE_NAME")?.GetValue(null)) ?? "";
            string channel = _websiteChannelContext.WebsiteChannelName;
            var builder = new ContentItemQueryBuilder()
                        .ForContentType(
                            // Scopes the query to pages of the 'My.ArticlePage' content type
                            pageType,
                            config => config
                                // Retrieves pages only from the specified channel
                                .Where(x => x.WhereEquals(nameof(IWebPageContentQueryDataContainer.WebPageItemID), id))
                                .ForWebsite(_websiteChannelContext.WebsiteChannelName)
                        // Retrieves only English variants of pages
                        ).InLanguage(language);

            // Executes the query and stores the data in generated 'ArticlePage' models
            IEnumerable<T> pages = await _progressiveCache.LoadAsync(async cs =>
            {
                if (cs.Cached)
                {
                    cs.CacheDependency = cacheKeys.GetCMSCacheDependency();
                }
                return await _executor.GetWebPageResult(
                                                        builder: builder,
                                                        resultSelector: _mapper.Map<T>,
                                                        options: new ContentQueryExecutionOptions { ForPreview = forPreview });
            }, new CacheSettings(CacheTiming.VeryLong, "GetFullPageItem", language, id, pageType, forPreview, channel));

            var page = pages.FirstOrDefault();

            var item = page != null ? new WebPageItem<TI>
            {
                WebPageItemGUID = page.SystemFields.WebPageItemGUID,
                WebPageItemID = page.SystemFields.WebPageItemID,
                WebPageItemName = page.SystemFields.WebPageItemName,
                WebPageItemOrder = page.SystemFields.WebPageItemOrder,
                WebPageItemParentID = page.SystemFields.WebPageItemParentID,
                WebPageItemTreePath = page.SystemFields.WebPageItemTreePath,
                WebPageUrlPath = page.SystemFields.WebPageUrlPath,
                WebPageRelativeUrl = (await _urlRetriever.Retrieve(page, language)).RelativePath,
                Data = dataMapper(page)
            } : new WebPageItem<TI>();

            return item;
        }
    }
}
