using CMS.ContentEngine;
using HBS.Xperience.Categories.Admin.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using StarterSite.Logic.Context;
using StarterSite.Logic.Repositories.Interfaces;
using StarterSite.RCL.Components.Widgets.FaqsWidget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X;

[assembly: RegisterWidget(
    identifier: "FaqsWidget",
    viewComponentType: typeof(FaqsViewComponent),
    name: "Faqs Widget",
    propertiesType: typeof(FaqsProperties),
    IconClass = "icon-question-circle",
    AllowCache = true)]

namespace StarterSite.RCL.Components.Widgets.FaqsWidget
{
    public class FaqsViewComponent : ViewComponent
    {
        private readonly IFaqRepository _faqRepository;
        private readonly ICacheScope _cacheScope;

        public FaqsViewComponent(IFaqRepository faqRepository, ICacheScope cacheScope)
        {
            _faqRepository = faqRepository;
            _cacheScope = cacheScope;
        }
        public async Task<IViewComponentResult> InvokeAsync(ComponentViewModel<FaqsProperties> properties)
        {
            // begin the collection of the cache keys
            _cacheScope.BeginWidget();
            if (properties.Properties.UseGroups)
            {
                var groups = await _faqRepository.GetFaqsGroups(properties.Properties.SelectedCategories);

                // add in the collected keys to the widgets cache depedencies
                properties.CacheDependencies.CacheKeys = _cacheScope.End();

                return View("~/Components/Widgets/FaqsWidget/_FaqGroupsWidget.cshtml", groups);
            } 
            else
            {
                var faqs = await _faqRepository.GetFaqsByCategory(properties.Properties.SelectedCategories);

                // add in the collected keys to the widgets cache depedencies
                properties.CacheDependencies.CacheKeys = _cacheScope.End();

                return View("~/Components/Widgets/FaqsWidget/_FaqsWidget.cshtml", faqs);
            }
        }
    }
    public class FaqsProperties : IWidgetProperties
    {
        
        [CategoryListFormComponent(Label = "Selected Categories", Order = 1)]
        public IEnumerable<int> SelectedCategories { get; set; } = Enumerable.Empty<int>();



        [CheckBoxComponent(Label = "Use Groups")]
        public bool UseGroups { get; set; } = false;
    }
}
