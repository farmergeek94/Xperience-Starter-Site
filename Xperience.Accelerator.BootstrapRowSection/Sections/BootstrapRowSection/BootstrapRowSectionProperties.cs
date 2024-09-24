using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xperience.Accelerator.BootstrapRowSectionShared;
using Xperience.Accelerator.BootstrapRowSectionShared.Models;
using Xperience.Accelerator.BootstrapRowSection.Components.Sections.BootstrapRowSection;
using Xperience.Accelerator.BootstrapRowSection.Repositories;

[assembly: RegisterSection("Xperience.Community.InlineBootstrapSection", "Bootstrap Section", propertiesType: typeof(BootstrapRowSectionProperties), customViewName: "~/Sections/BootstrapRowSection/_BootstrapRowSection.cshtml", Description = "A customizable bootstrap row", IconClass = "icon-layout")]


namespace Xperience.Accelerator.BootstrapRowSection.Components.Sections.BootstrapRowSection
{
    [FormCategory(Label = "Columns", Order = 1)]
    [FormCategory(Label = "Row", Order = 4, Collapsible = true, IsCollapsed = true)]
    [FormCategory(Label = "Container", Order = 8, Collapsible = true, IsCollapsed = true)]
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
        
        const string RowDirectionDataSource = @";Left to Right
flex-row-reverse;Right to Left";

        [DropDownComponent(Label = "Prefix", Options = PrependDataSource, Order = 2)]
        public string Prefix { get; set; } = "col-md";

        [BootstrapRowFormComponent(Order = 3)]
        public IEnumerable<BootstrapColumnModel> Columns { get; set; } = Enumerable.Empty<BootstrapColumnModel>();

        [DropDownComponent(Label = "Row Direction", Options = RowDirectionDataSource, Order = 5, ExplanationText = "Allows for off setting rows that still display correctly on mobile.")]
        public string RowDirection { get; set; } = "";

        [TextInputComponent(Label = "Row Custom Css", Order = 6)]
        public string RowCustomCss { get; set; } = "";

        [CheckBoxComponent(Label = "No Padding", Order = 7)]
        public bool NoPadding { get; set; } = false;

        [DropDownComponent(Label = "Container", Options = ContainerDataSource, Order = 9)]
        public string Container { get; set; } = "";

        [DropDownComponent(Label = "Container Background", Order = 10, DataProviderType = typeof(BootstrapRowContainerClasses))]
        public string ContainerBackground { get; set; } = "";

        [TextAreaComponent(Label = "Html Before", Order = 11)]
        public string HtmlBefore { get; set; } = "";

        [TextAreaComponent(Label = "Html After", Order = 12)]
        public string HtmlAfter { get; set; } = "";
    }
}
