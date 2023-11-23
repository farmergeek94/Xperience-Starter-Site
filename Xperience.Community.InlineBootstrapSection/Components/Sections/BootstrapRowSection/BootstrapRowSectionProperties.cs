using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xperience.Community.InlineBootstrapSection.Components.FormComponents.BootstrapRowFormComponent;
using Xperience.Community.InlineBootstrapSection.Components.Sections.InlineBootstrapSection;
using Xperience.Community.InlineBootstrapSection.Models;

[assembly: RegisterSection("Xperience.Community.InlineBootstrapSection", "Bootstrap Section", propertiesType: typeof(BootstrapRowSectionProperties), customViewName: "~/Components/Sections/BootstrapRowSection/_BootstrapRowSection.cshtml", Description = "A customizable bootstrap row", IconClass = "icon-layout")]


namespace Xperience.Community.InlineBootstrapSection.Components.Sections.InlineBootstrapSection
{
    [FormCategory(Label = "Columns", Order = 1)]
    [FormCategory(Label = "Container", Order = 4, Collapsible = true, IsCollapsed = true)]
    public class BootstrapRowSectionProperties : ISectionProperties
    {
        const string PrependDataSource = @"col;Extra Small (0px to 575px)
col-sm;Small (576px to 767px)
col-md;Medium (768px to 991px)
col-lg;Large (992px to 1199px)
col-xl;Extra Large (1200px to 1399px)
col-xxl;Extra extra large (1400px)";

        const string ContainerDataSource = @";None
container;Container
container-fluid;Container Fluid";

        [DropDownComponent(Label = "Prefix", Options = PrependDataSource, Order = 2)]
        public string Prefix { get; set; } = "col-md";

        [BootstrapRowFormComponent(Order = 3)]
        public IEnumerable<BootstrapColumnModel> Columns { get; set; } = Enumerable.Empty<BootstrapColumnModel>();

        [DropDownComponent(Label = "Container", Options = ContainerDataSource, Order = 5)]
        public string Container { get; set; } = "";

        [TextAreaComponent(Label = "Html Before", Order = 7)]
        public string HtmlBefore { get; set; } = "";

        [TextAreaComponent(Label = "Html After", Order = 7)]
        public string HtmlAfter { get; set; } = "";
    }
}
