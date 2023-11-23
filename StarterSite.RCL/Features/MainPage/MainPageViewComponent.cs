using Kentico.Content.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Http;
using StarterSite.Logic.Repositories.Interfaces;
using StarterSite.Models;
using X;

namespace StarterSite.RCL.Features.MainPage
{
    public class MainPageViewComponent : ViewComponent
    {
        private readonly IWebPageRepository _webPageRepository;

        public MainPageViewComponent(IWebPageRepository webPageRepository)
        {
            _webPageRepository = webPageRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var page = await _webPageRepository.GetCurrentPageItem<Page, PageItem>(x => new PageItem {
                UrlPath = x.SystemFields.WebPageUrlPath 
            });

            return View("~/Features/MainPage/MainPage.cshtml", page);
        }
    }
}
