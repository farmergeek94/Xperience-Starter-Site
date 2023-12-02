using StarterSite.Logic.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterSite.RCL.Components.Navigation
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly INavigationRepository _navigationRepository;

        public NavigationViewComponent(INavigationRepository navigationRepository)
        {
            _navigationRepository = navigationRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(string path, string? channel = null)
        {
            var navItems = await _navigationRepository.GetNavigationItems(path, channel);
            return View("_NavigationItems", navItems);
        }
    }
}
