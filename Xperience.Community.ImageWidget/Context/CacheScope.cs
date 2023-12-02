using CMS.Websites.Routing;
using Kentico.Content.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xperience.Community.ImageWidget.Context
{
    public class CacheScope : ICacheScope
    {
        public CacheScope(IWebPageDataContextRetriever webPageDataContextRetriever, IWebsiteChannelContext websiteChannelContext)
        {
            _webPageDataContextRetriever = webPageDataContextRetriever;
            _websiteChannelContext = websiteChannelContext;
        }
        private class CacheScopeItem
        {
            public readonly HashSet<string> Dependancies = new(StringComparer.InvariantCultureIgnoreCase);

            public CacheScopeItem? ParentItem { get; set; } = null;

            public void Add(string[] dependancies)
            {
                Dependancies.UnionWith(dependancies);
                if(ParentItem != null)
                {
                    ParentItem.Add(dependancies);
                }
            }
        }

        private CacheScopeItem? _currentItem = null;
        private readonly IWebPageDataContextRetriever _webPageDataContextRetriever;
        private readonly IWebsiteChannelContext _websiteChannelContext;

        public void Begin()
        {
            if (_currentItem == null)
            {
                _currentItem = new CacheScopeItem();
            }
            else
            {
                var item = new CacheScopeItem();
                item.ParentItem = _currentItem;
                _currentItem = item;
            }
        }

        public void BeginWidget()
        {
            Begin();
        }

        public void Add(string dependancy)
        {
            Add(new string[] { dependancy });
        }
        public void Add(IEnumerable<string> dependancies)
        {
            Add(dependancies.ToArray());
        }
        public void Add(string[] dependancies)
        {
            if (_currentItem == null)
            {
                return;
            }
            _currentItem.Add(dependancies);
        }

        public string[] End()
        {
            var item = _currentItem;
            if (item != null)
            {
                _currentItem = _currentItem.ParentItem;
                return item.Dependancies.ToArray();
            }
            return Array.Empty<string>();
        }

    }
}
