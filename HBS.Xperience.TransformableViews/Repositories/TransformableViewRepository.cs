using CMS.DataEngine;
using CMS.Helpers;
using HBS.TransformableViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViews.Repositories
{
    internal class TransformableViewRepository : ITransformableViewRepository
    {
        private readonly IProgressiveCache _progressiveCache;
        private readonly ITransformableViewInfoProvider _transformableViewInfoProvider;

        public TransformableViewRepository(IProgressiveCache progressiveCache, ITransformableViewInfoProvider transformableViewInfoProvider)
        {
            _progressiveCache = progressiveCache;
            _transformableViewInfoProvider = transformableViewInfoProvider;
        }
        public ITransformableViewItem? GetTransformableView(string viewName, bool update = false)
        {
            var view = _progressiveCache.Load(cs =>
            {
                if (cs.Cached)
                {
                    cs.CacheDependency = CacheHelper.GetCacheDependency(new string[] {
                            $"{TransformableViewInfo.OBJECT_TYPE}|all"
                        });
                }
                return _transformableViewInfoProvider.Get()
                .Where(w => w.WhereEquals(nameof(TransformableViewInfo.TransformableViewName), viewName)).FirstOrDefault();
            }, new CacheSettings(86400, "GetTransformableViewInfo", viewName));

            if (view != null && update)
            {
                // Run direct in order to no trigger cache dependancy.
                ConnectionHelper.ExecuteNonQuery($@"
                    update {TransformableViewInfo.TYPEINFO.ClassStructureInfo.TableName} 
                    set {nameof(TransformableViewInfo.TransformableViewLastRequested)} = GETDATE() 
                    where {nameof(TransformableViewInfo.TransformableViewName)} = @ViewName
                ", new QueryDataParameters
                {
                    new DataParameter("@ViewName", viewName)
                });
            }

            return view;
        }

        public IEnumerable<string> GetTransformableViewNames()
        {
            return _progressiveCache.Load(cs =>
            {
                if (cs.Cached)
                {
                    cs.CacheDependency = CacheHelper.GetCacheDependency(new string[] {
                            $"{TransformableViewInfo.OBJECT_TYPE}|all"
                        });
                }
                return _transformableViewInfoProvider.Get()
                .Columns(nameof(TransformableViewInfo.TransformableViewName)).Select(x => x.TransformableViewName);
            }, new CacheSettings(86400 * 365, "GetTransformableViewInfoNames"));
        }
    }
}
