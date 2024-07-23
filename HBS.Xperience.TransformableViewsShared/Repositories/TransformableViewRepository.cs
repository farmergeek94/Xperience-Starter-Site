using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;
using HBS.TransformableViews;
using HBS.TransformableViews_Experience;
using HBS.Xperience.TransformableViewsShared.Library;
using HBS.Xperience.TransformableViewsShared.Models;
using HBS.Xperience.TransformableViewsShared.Services;
using Kentico.Xperience.Admin.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViewsShared.Repositories
{
    internal class TransformableViewRepository : ITransformableViewRepository
    {
        private readonly IProgressiveCache _progressiveCache;
        private readonly ITransformableViewInfoProvider _transformableViewInfoProvider;
        private readonly IEncryptionService _encryptionService;
        private readonly ICacheService _cacheService;

        public TransformableViewRepository(IProgressiveCache progressiveCache, ITransformableViewInfoProvider transformableViewInfoProvider, IEncryptionService encryptionService, ICacheService cacheService)
        {
            _progressiveCache = progressiveCache;
            _transformableViewInfoProvider = transformableViewInfoProvider;
            _encryptionService = encryptionService;
            _cacheService = cacheService;
        }

        public Dictionary<string, DateTime> LastViewedDates { get; set; } = new Dictionary<string, DateTime>();

        public ITransformableViewItem? GetTransformableView(string viewName, bool update = false)
        {
            var view = _progressiveCache.Load(cs =>
            {                
                var view = _transformableViewInfoProvider.Get().Where(w => w.WhereEquals(nameof(TransformableViewInfo.TransformableViewName), viewName)).ToArray().FirstOrDefault();

                if (cs.Cached)
                {
                    cs.CacheDependency = _cacheService.GetCacheDependencies(new string[] {
                        view == null ? $"{TransformableViewInfo.OBJECT_TYPE}|all" : $"{TransformableViewInfo.OBJECT_TYPE}|byid|{view?.TransformableViewID}"
                    });
                }
                return view == null ? null : _encryptionService.DecryptView(view);
            }, new CacheSettings(86400, "GetTransformableViewInfo", viewName));

            if (view != null && update)
            {
                LastViewedDates[viewName] = DateTime.Now;
            }

            return view;
        }

        public async Task<IEnumerable<SelectListItem>> GetTransformableViewSelectItems()
        {
            var names = await TransformableViewNames();
            return names.Where(x => x.TransformableViewTypeEnum == TransformableViewTypeEnum.Transformable).Select(x=>new SelectListItem(x.TransformableViewDisplayName, x.TransformableViewName));
        }

        public async Task<IEnumerable<SelectListItem>> GetTransformableViewObjectSelectItems()
        {
            var names = await TransformableViewNames();
            return names.Where(x=>x.TransformableViewTypeEnum == TransformableViewTypeEnum.Listing).Select(x => new SelectListItem(x.TransformableViewDisplayName, x.TransformableViewName));
        }

        private async Task<IEnumerable<TransformableViewInfo>> TransformableViewNames()
        {
            return await _progressiveCache.LoadAsync(async (cs) =>
            {
                if (cs.Cached)
                {
                    cs.CacheDependency = _cacheService.GetCacheDependencies([
                            $"{TransformableViewInfo.OBJECT_TYPE}|all"
                        ]);
                }
                return await _transformableViewInfoProvider.Get()
                .Columns(nameof(TransformableViewInfo.TransformableViewName), nameof(TransformableViewInfo.TransformableViewDisplayName), nameof(TransformableViewInfo.TransformableViewType)).GetEnumerableTypedResultAsync();
            }, new CacheSettings(86400 * 365, "GetTransformableViewInfoNames"));
        }
    }
}
