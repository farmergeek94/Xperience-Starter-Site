using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterSite.Logic.Repositories.Implementations
{
    public class PageBuilderRepository : IPageBuilderRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPreferredLanguageRetriever _preferredLanguageRetriever;
        private readonly IWebPageDataContextRetriever _webPageDataContextRetriever;

        public PageBuilderRepository(IHttpContextAccessor httpContextAccessor, IPreferredLanguageRetriever preferredLanguageRetriever, IWebPageDataContextRetriever webPageDataContextRetriever)
        {
            _httpContextAccessor = httpContextAccessor;
            _preferredLanguageRetriever = preferredLanguageRetriever;
            _webPageDataContextRetriever = webPageDataContextRetriever;
        }

        public bool IsPreview => _httpContextAccessor.HttpContext.Kentico().Preview().Enabled;

        public string Language => _preferredLanguageRetriever.Get();

        public RoutedWebPage PageContext => _webPageDataContextRetriever.Retrieve().WebPage;
    }
}
