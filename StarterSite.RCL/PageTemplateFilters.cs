using CMS.DataEngine;
using Kentico.PageBuilder.Web.Mvc;
using StarterSite.RCL.Features.MainPageTemplate;
using StarterSite.RCL.Library.PageTemplateFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module = CMS.DataEngine.Module;

namespace StarterSite.RCL
{
    public class PageTemplateFilters : Module
    {
        // Module class constructor, inherits from the base constructor with the code name of the module as the parameter
        public PageTemplateFilters() : base("PageTemplateFilters")
        {
        }

        // Initializes the module. Called when the application starts.
        protected override void OnInit()
        {
            base.OnInit();

            RegisterPageTemplateFilters();
        }

        private void RegisterPageTemplateFilters()
        {
            //PageBuilderFilters.PageTemplates.Add(new MainPageTemplateFilter());
        }
    }
}
