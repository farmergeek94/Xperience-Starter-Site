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
        public async Task<IEnumerable<FaqItem>> GetFaqs(IEnumerable<Guid>? groupIds = null, string language = "en")
        {
            if (groupIds != null)
            {
                return (await GetFaqsGroups(groupIds, language)).SelectMany(x=>x.Faqs).OrderBy(x=>x.FaqQuestion);
            }
            return await GetAllFaqs(language);

        }

        public async Task<IEnumerable<GroupItem>> GetFaqsGroups(IEnumerable<Guid> groupIds, string language = "en")
        {
            // Setup cache keys and add them to the scope.
            var cacheKeys = _cacheKeyService.Create();

            var builder = new ContentItemQueryBuilder()
                       .ForContentType(
                           Group.CONTENT_TYPE_NAME,
                           config => config
                               .Columns(nameof(Group.Title), nameof(Group.Faqs), nameof(IContentQueryDataContainer.ContentItemID))
                               .Where(x => x.WhereIn(nameof(IContentQueryDataContainer.ContentItemGUID), groupIds.ToArray()))
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
                    }
                }
                if (cs.Cached)
                {
                    cs.CacheDependency = cacheKeys.GetCMSCacheDependency();
                }
                return groups;
            }, new CacheSettings(CacheTiming.VeryLong, "GetGroupFaqs", string.Join('|', groupIds), language));

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
