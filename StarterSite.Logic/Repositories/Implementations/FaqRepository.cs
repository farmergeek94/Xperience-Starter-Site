using CMS.ContentEngine;
using CMS.Helpers;
using StarterSite.Logic.Library;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using X;
using HBS.Xperience.Categories;

namespace StarterSite.Logic.Repositories.Implementations
{
    public class FaqRepository : IFaqRepository
    {
        private readonly IContentQueryExecutor _executor;
        private readonly IContentQueryResultMapper _contentQueryResultMapper;
        private readonly IProgressiveCache _progressiveCache;
        private readonly ICacheKeyService _cacheKeyService;

        public FaqRepository(IContentQueryExecutor executor, IContentQueryResultMapper contentQueryResultMapper, IProgressiveCache progressiveCache, ICacheKeyService cacheKeyService)
        {
            _executor = executor;
            _contentQueryResultMapper = contentQueryResultMapper;
            _progressiveCache = progressiveCache;
            _cacheKeyService = cacheKeyService;
        }
        public async Task<IEnumerable<FaqItem>> GetFaqs(IEnumerable<int>? categories = null, string language = "en")
        {
            if (categories != null)
            {
                return (await GetFaqsGroups(categories, language)).SelectMany(x=>x.Faqs).OrderBy(x=>x.FaqQuestion);
            }
            return await GetAllFaqs(language);

        }

        public async Task<IEnumerable<GroupItem>> GetFaqsGroups(IEnumerable<int> categories, string language = "en")
        {
            // Setup cache keys and add them to the scope.
            var cacheKeys = _cacheKeyService.Create();


            var builder = new ContentItemQueryBuilder()
                       .ForContentType(
                           Group.CONTENT_TYPE_NAME,
                           config => config
                               .Columns(nameof(Group.Title), nameof(Group.Faqs), nameof(IContentQueryDataContainer.ContentItemID))
                               .Where(x =>
                               {
                                   x.ContentItemCategories(categories);
                               })
                               .OrderBy(nameof(Group.Title))
                               .WithLinkedItems(1)
                       ).InLanguage(language);


            // Executes the configured query
            IEnumerable<Group> query = await _progressiveCache.LoadAsync(async cs =>
            {
                var groups = await _executor.GetResult(
                                                        builder: builder,
                                                        resultSelector: _contentQueryResultMapper.Map<Group>);
                foreach(var group in groups)
                {
                    cacheKeys.ContentItem(group.SystemFields.ContentItemID);
                    foreach (var item in group.Faqs)
                    {
                        cacheKeys.ContentItem(item.SystemFields.ContentItemID);
                        foreach (var cat in categories)
                        {
                            cacheKeys.CustomKey($"{ContentItemCategoryInfo.OBJECT_TYPE}|categoryid|{cat}|contentitemid|{item.SystemFields.ContentItemID}");
                        }
                    }
                    foreach (var cat in categories)
                    {
                        cacheKeys.CustomKey($"{ContentItemCategoryInfo.OBJECT_TYPE}|categoryid|{cat}|contentitemid|{group.SystemFields.ContentItemID}");
                    }
                }
                if (cs.Cached)
                {
                    cs.CacheDependency = cacheKeys.GetCMSCacheDependency();
                }
                return groups;
            }, new CacheSettings(CacheTiming.VeryLong, "GetGroupFaqs", string.Join('|', categories), language));

            return query.Select(x => new GroupItem
            {
                Title = x.Title,
                Faqs = x.Faqs.Select(f => new FaqItem
                {
                    FaqQuestion = f.FaqQuestion,
                    FaqAnswer = f.FaqAnswer
                }).OrderBy(x=>x.FaqQuestion)
            });
        }

        public async Task<IEnumerable<FaqItem>> GetFaqsByCategory(IEnumerable<int> categories, string language = "en")
        {
            // Setup cache keys and add them to the scope.
            var cacheKeys = _cacheKeyService.Create();

            var builder = new ContentItemQueryBuilder()
                       .ForContentType(
                           Faq.CONTENT_TYPE_NAME,
                           config =>
                           {
                               config
                               .Columns(nameof(Faq.FaqQuestion), nameof(Faq.FaqAnswer))
                               .Where(x=>x.ContentItemCategories(categories))
                               .OrderBy(nameof(Faq.FaqQuestion));
                           }
                       ).InLanguage(language);


            // Executes the configured query
            IEnumerable<Faq> query = await _progressiveCache.LoadAsync(async cs =>
            {
                var faqs = await _executor.GetResult(
                                                        builder: builder,
                                                        resultSelector: _contentQueryResultMapper.Map<Faq>);

                foreach (var item in faqs)
                {
                    cacheKeys.ContentItem(item.SystemFields.ContentItemID);
                    foreach (var cat in categories)
                    {
                        cacheKeys.CustomKey($"{ContentItemCategoryInfo.OBJECT_TYPE}|categoryid|{cat}|contentitemid|{item.SystemFields.ContentItemID}");
                    }
                }

                if (cs.Cached)
                {
                    cs.CacheDependency = cacheKeys.GetCMSCacheDependency();
                }
                return faqs;
            }, new CacheSettings(CacheTiming.VeryLong, "GetFaqs", string.Join(',', categories), language));

            return query.Select(x => new FaqItem
            {
                FaqQuestion = x.FaqQuestion,
                FaqAnswer = x.FaqAnswer
            });
        }

        private async Task<IEnumerable<FaqItem>> GetAllFaqs(string language = "en")
        {
            // Setup cache keys and add them to the scope.
            var cacheKeys = _cacheKeyService.Create();
            cacheKeys.ContentItems(Faq.CONTENT_TYPE_NAME);

            var builder = new ContentItemQueryBuilder()
                       .ForContentType(
                           Faq.CONTENT_TYPE_NAME,
                           config =>
                           {
                               config
                               .Columns(nameof(Faq.FaqQuestion), nameof(Faq.FaqAnswer))
                               .OrderBy(nameof(Faq.FaqQuestion));
                           }
                       ).InLanguage(language);


            // Executes the configured query
            IEnumerable<Faq> query = await _progressiveCache.LoadAsync(async cs =>
            {
                if (cs.Cached)
                {
                    cs.CacheDependency = cacheKeys.GetCMSCacheDependency();
                }
                return await _executor.GetResult(
                                                        builder: builder,
                                                        resultSelector: _contentQueryResultMapper.Map<Faq>);
            }, new CacheSettings(CacheTiming.VeryLong, "GetAllFaqs", language));

            return query.Select(x => new FaqItem
            {
                FaqQuestion = x.FaqQuestion,
                FaqAnswer = x.FaqAnswer
            });
        }
    }
}
