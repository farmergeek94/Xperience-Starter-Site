using CMS.ContentEngine;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using StarterSite.Logic.Context;
using StarterSite.Logic.Repositories.Interfaces;
using StarterSite.RCL.Components.Widgets.Faqs;
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

namespace StarterSite.RCL.Components.Widgets.Faqs
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

            var groups = await _faqRepository.GetFaqsGroups(properties.Properties.SelectedGroups.Select(x=>x.Identifier));

            // add in the collected keys to the widgets cache depedencies
            properties.CacheDependencies.CacheKeys = _cacheScope.End();

            return View("~/Components/Widgets/Faqs/_Faqs.cshtml", new FaqsWidgetModel
            {
                ShowGroupHeaders = properties.Properties.ShowGroupHeaders,
                Groups = groups
            });
        }
    }
    public class FaqsProperties : IWidgetProperties
    {
        
        [ContentItemSelectorComponent(Group.CONTENT_TYPE_NAME, Label = "Selected Faq Groups", Order = 1, AllowContentItemCreation = false)]
        public IEnumerable<ContentItemReference> SelectedGroups { get; set; } = new List<ContentItemReference>();

        [CheckBoxComponent(Label = "Show Group Headers")]
        public bool ShowGroupHeaders { get; set; } = false;
    }
}
