using Kentico.Content.Web.Mvc;
using StarterSite.Logic.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X;

namespace StarterSite.RCL.Features.Footer
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly IWebPageDataContextInitializer _webPageDataContextInitializer;

        public FooterViewComponent(IWebPageDataContextInitializer webPageDataContextInitializer)
        {
            _webPageDataContextInitializer = webPageDataContextInitializer;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            _webPageDataContextInitializer.Initialize(new RoutedWebPage { LanguageName = "en", ContentTypeName = Page.CONTENT_TYPE_NAME, WebPageItemID = 11 });
            return View("~/Features/Footer/Footer.cshtml");
        }
    }
}
