using CMS.Helpers;
using CMS.Websites.Routing;
using Xperience.Community.ImageWidget.Context;

namespace Xperience.Community.ImageWidget.Library
{
    /// <summary>
    /// Helper Method that allows you to store cache dependencies easily, and also be able to apply them to the IPageRetriever, should NOT inject this but instead declare it since otherwise the cacheKeys will be the same each time.
    /// </summary>
    public class CacheKeyHelper
    {
        private readonly HashSet<string> cacheKeys = new(StringComparer.InvariantCultureIgnoreCase);
        private readonly IWebsiteChannelContext _context;
        private readonly ICacheScope _cacheScope;

        public CacheKeyHelper(IWebsiteChannelContext context, ICacheScope cacheScope)
        {
            _context = context;
            _cacheScope = cacheScope;
        }

        public ISet<string> GetKeys() => cacheKeys;

        private void Add(string key)
        {
            Add(new string[] { key });
            _cacheScope.Add(key);
        }

        private void Add(IEnumerable<string> keys)
        {
            cacheKeys.UnionWith(keys);
            _cacheScope.Add(keys);
        }

        public CacheKeyHelper CustomKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return this;
            }

            Add(key);

            return this;
        }
        public CacheKeyHelper CustomKeys(IEnumerable<string> keys)
        {
            Add(keys);

            return this;
        }

        public CacheKeyHelper ContentItem(int? nodeId) => ContentItem(nodeId.GetValueOrDefault());
        public CacheKeyHelper ContentItem(int nodeId)
        {
            if (nodeId <= 0)
            {
                return this;
            }

            Add($"contentitem|byid|{nodeId}");

            return this;
        }

        public CacheKeyHelper ContentItem(Guid? nodeGUID) => ContentItem(nodeGUID.GetValueOrDefault());
        public CacheKeyHelper ContentItem(Guid nodeGUID)
        {
            if (nodeGUID == default)
            {
                return this;
            }

            Add($"contentitem|byguid|{nodeGUID}");

            return this;
        }

        public CacheKeyHelper ContentItems(string itemType)
        {
            if (string.IsNullOrWhiteSpace(itemType))
            {
                return this;
            }

            Add($"contentitem|bycontenttype|{itemType}");

            return this;
        }

        public CacheKeyHelper Attachment(Guid? attachmentGUID) => Attachment(attachmentGUID.GetValueOrDefault());
        public CacheKeyHelper Attachment(Guid attachmentGUID)
        {
            if (attachmentGUID == default)
            {
                return this;
            }

            Add($"attachment|{attachmentGUID}");

            return this;
        }

        public CacheKeyHelper Media(Guid? mediaFileGUID) => Media(mediaFileGUID.GetValueOrDefault());
        public CacheKeyHelper Media(Guid mediaFileGUID)
        {
            if (mediaFileGUID == default)
            {
                return this;
            }

            Add($"mediafile|{mediaFileGUID}");

            return this;
        }

        public CacheKeyHelper Collection<T>(IEnumerable<T> items, Action<T, CacheKeyHelper> action)
        {
            foreach (var item in items.Where(x => x != null))
            {
                action(item, this);
            }

            return this;
        }


        public CacheKeyHelper Collection<T>(IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items.Where(x => x != null))
            {
                action(item);
            }

            return this;
        }

        public CacheKeyHelper ApplyDependenciesTo(Action<string> action)
        {
            foreach (string cacheKey in cacheKeys)
            {
                action(cacheKey);
            }
            return this;
        }

        public CacheKeyHelper ApplyAllDependenciesTo(Action<string[]> action)
        {
            action(cacheKeys.ToArray());
            return this;
        }

        public CacheKeyHelper Clear()
        {
            cacheKeys.Clear();
            return this;
        }

        public CMSCacheDependency GetCMSCacheDependency()
        {
            return CacheHelper.GetCacheDependency(cacheKeys.ToArray());
        }
    }
}
