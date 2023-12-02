using CMS.Websites.Routing;
using StarterSite.Logic.Context;
using StarterSite.Logic.Library;
using StarterSite.Logic.Services.Interfaces;
using StarterSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterSite.Logic.Services.Implmentations
{
    public class CacheKeyService : ICacheKeyService
    {
        private readonly IWebsiteChannelContext _websiteChannelContext;
        private readonly ICacheScope _cacheScope;

        public CacheKeyService(IWebsiteChannelContext websiteChannelContext, ICacheScope cacheScope)
        {
            _websiteChannelContext = websiteChannelContext;
            _cacheScope = cacheScope;
        }
        public CacheKeyHelper Create()
        {
            return new CacheKeyHelper(_websiteChannelContext, _cacheScope);
        }
    }
}
