using Kentico.PageBuilder.Web.Mvc.PageTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X;

namespace StarterSite.RCL.Library.PageTemplateFilters
{
    public class MainPageTemplateFilter: IPageTemplateFilter
    {
        public IEnumerable<PageTemplateDefinition> Filter(IEnumerable<PageTemplateDefinition> pageTemplates, PageTemplateFilterContext context)
        {
            // Applies filtering to a collection of page templates based on the language of the currently edited page
            if (context.ContentTypeName == Page.CONTENT_TYPE_NAME)
            {
                // Filters the collection to only contain templates for Spanish pages
                return pageTemplates.Where(t => t.Identifier.StartsWith(Page.CONTENT_TYPE_NAME));
            }

            // Excludes all Spanish page templates from the collection if the context does not match this filter
            // Assumes that the categories of page templates are mutually exclusive
            return pageTemplates.Where(t => !t.Identifier.StartsWith(Page.CONTENT_TYPE_NAME));
        }
    }
}
