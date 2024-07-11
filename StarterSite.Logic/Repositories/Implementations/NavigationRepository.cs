using CMS.ContentEngine;
using CMS.Helpers;
using CMS.Websites;
using CMS.Websites.Routing;
using StarterSite.Logic.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X;

namespace StarterSite.Logic.Repositories.Implementations
{
    public class NavigationRepository : INavigationRepository
    {
        private readonly IPageBuilderRepository _pageBuilderRepository;
        private readonly IProgressiveCache _progressiveCache;
        private readonly IWebsiteChannelContext _websiteChannelContext;
        private readonly IContentQueryExecutor _executor;
        private readonly IWebPageQueryResultMapper _mapper;
        private readonly ICacheKeyService _cacheKeyService;

        public NavigationRepository(IPageBuilderRepository pageBuilderRepository, IProgressiveCache progressiveCache, IWebsiteChannelContext websiteChannelContext, IContentQueryExecutor executor, IWebPageQueryResultMapper mapper, ICacheKeyService cacheKeyService)
        {
            _pageBuilderRepository = pageBuilderRepository;
            _progressiveCache = progressiveCache;
            _websiteChannelContext = websiteChannelContext;
            _executor = executor;
            _mapper = mapper;
            _cacheKeyService = cacheKeyService;
        }
        public async Task<IEnumerable<NavigationItem>> GetNavigationItems(string path, string? channel = null)
        {
            var preview = _pageBuilderRepository.IsPreview;
            var language = _pageBuilderRepository.Language;
            var channelName = channel ?? _websiteChannelContext.WebsiteChannelName;

            var cacheKeys = _cacheKeyService.Create()
                .WebPageType(Navitem.CONTENT_TYPE_NAME);

            var builder = new ContentItemQueryBuilder()
                .ForContentType(Navitem.CONTENT_TYPE_NAME, config => config.ForWebsite(channel))
                .InLanguage(language);
            // Executes the query and stores the data in generated 'ArticlePage' models
            IEnumerable<Navitem> pages = await _progressiveCache.LoadAsync(async cs =>
            {
                if(cs.Cached)
                {
                    cs.CacheDependency = cacheKeys.GetCMSCacheDependency();
                }
                return await _executor.GetWebPageResult(
                                                        builder: builder,
                                                        resultSelector: _mapper.Map<Navitem>,
                                                        options: new ContentQueryExecutionOptions { ForPreview = preview });

            }, new CacheSettings(CacheTiming.VeryLong, "GetNavigationItems", language, path, channelName, preview));

            var navItems = pages.Select(x => new NavigationItem
            {
                ID = x.SystemFields.WebPageItemID,
                ParentID = x.SystemFields.WebPageItemParentID,
                Title = x.Name,
                Url = x.Url,
                UrlType = x.UrlType
            }).ToArray();

            // Get the root parentitemid
            var rootParentID = navItems.Min(x => x.ParentID);

            // Create the lookup.  
            var childItems = navItems.ToLookup(x => x.ParentID);

            // Assign the children to the parents
            foreach (var item in navItems)
            {
                item.Children = childItems[item.ID].ToList();
            }

            // Get the root level items. 
            return childItems[rootParentID].ToList();
        }
    }
}
