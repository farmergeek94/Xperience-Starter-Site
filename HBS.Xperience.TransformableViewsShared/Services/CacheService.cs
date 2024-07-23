using CMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViewsShared.Services
{
    internal class CacheService : ICacheService
    {
        List<string> _keys = new List<string>();
        public ICacheService Add(IEnumerable<string> keys)
        {
            _keys.AddRange(keys);
            return this;
        }

        public IEnumerable<string> GetDependenciesList()
        {
            return _keys;
        }

        public CMSCacheDependency GetCacheDependencies(string key)
        {
            _keys.Add(key);
            return CacheHelper.GetCacheDependency(key);
        }
        public CMSCacheDependency GetCacheDependencies(IEnumerable<string> keys)
        {
            _keys.AddRange(keys);
            return CacheHelper.GetCacheDependency(keys.ToArray());
        }
    }
}
