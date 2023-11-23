using CMS.DataEngine;
using CMS.Helpers;
using CMS.Websites;
using CMS.Websites.Routing;
using StarterSite.Logic.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterSite.Models
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

        public CacheKeyHelper ObjectType(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                return this;
            }

            Add($"{typeName}|all");

            return this;
        }

        public CacheKeyHelper WebPageType(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                return this;
            }

            Add($"webpageitem|bychannel|{_context.WebsiteChannelName}|bycontenttype|{className}");

            return this;
        }

        public CacheKeyHelper Object(string objectType, int? id) => Object(objectType, id.GetValueOrDefault());
        public CacheKeyHelper Object(string objectType, int id)
        {
            if (string.IsNullOrWhiteSpace(objectType) || id <= 0)
            {
                return this;
            }

            Add($"{objectType}|byid|{id}");

            return this;
        }

        public CacheKeyHelper Object(string objectType, Guid? guid) => Object(objectType, guid.GetValueOrDefault());
        public CacheKeyHelper Object(string objectType, Guid guid)
        {
            if (guid == default)
            {
                return this;
            }

            Add($"{objectType}|byguid|{guid}");

            return this;
        }

        public CacheKeyHelper Object(string objectType, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return this;
            }

            Add($"{objectType}|byname|{name}");

            return this;
        }

        public CacheKeyHelper Objects(IEnumerable<BaseInfo> objects)
        {
            Add(objects.Select(o => $"{o.TypeInfo.ObjectType}|byid|{o.Generalized.ObjectID}"));

            return this;
        }

        /// <summary>
        /// Add a dependancy based on a string
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public CacheKeyHelper Page(string path, PathTypeEnum? type = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return this;
            }

            switch (type)
            {
                case PathTypeEnum.Single:
                    Add($"webpageitem|bychannel|{_context.WebsiteChannelName}|bypath|{path}");
                    break;
                case PathTypeEnum.Children:
                    Add($"webpageitem|bychannel|{_context.WebsiteChannelName}|childrenofpath|{path}");
                    break;
                case PathTypeEnum.Section:
                    Add($"webpageitem|bychannel|{_context.WebsiteChannelName}|bypath|{path}");
                    Add($"webpageitem|bychannel|{_context.WebsiteChannelName}|childrenofpath|{path}");
                    break;
                case null:
                default:
                    if (path.EndsWith("/%"))
                    {
                        Add($"webpageitem|bychannel|{_context.WebsiteChannelName}|childrenofpath|{path}");
                    }
                    else
                    {
                        Add($"webpageitem|bychannel|{_context.WebsiteChannelName}|bypath|{path}");
                    }
                    break;
            }

            return this;
        }


        public CacheKeyHelper Pages(IEnumerable<IWebPageFieldsSource> pages) => Pages(pages.Select(x => x.SystemFields.WebPageItemID));

        public CacheKeyHelper Page(int? itemId) => Page(itemId.GetValueOrDefault());
        public CacheKeyHelper Page(int itemId)
        {
            if (itemId <= 0)
            {
                return this;
            }

            Add($"webpageitem|byid|{itemId}");

            return this;
        }



        public CacheKeyHelper Page(Guid? documentGUID) => Page(documentGUID.GetValueOrDefault());
        public CacheKeyHelper Page(Guid documentGUID)
        {
            if (documentGUID == default)
            {
                return this;
            }

            Add($"webpageitem|byguid|{documentGUID}");

            return this;
        }

        public CacheKeyHelper Pages(IEnumerable<int?> itemIds) => Pages(itemIds.Where(x => x.HasValue).Select(x => x.Value));
        public CacheKeyHelper Pages(IEnumerable<int> itemIds)
        {
            Add(itemIds.Select(p => $"webpageitem|byid|{p}"));

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
