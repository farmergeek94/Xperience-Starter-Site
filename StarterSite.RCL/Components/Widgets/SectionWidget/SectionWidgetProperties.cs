using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations.Internal;
using StarterSite.RCL.Components.Widgets.SectionWidget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: RegisterWidget($"{nameof(SectionWidgetProperties)}.IDENTIFIER", "Section", typeof(SectionWidgetProperties), "~/Components/Widgets/SectionWidget/SectionWidget.cshtml", Description = "Adds another section to the page", IconClass = "icon-box", AllowCache = false)]


namespace StarterSite.RCL.Components.Widgets.SectionWidget
{
    public class SectionWidgetProperties : IWidgetProperties
    {
        [CodeNameComponent(Disabled = true)]
        public string AreaIdentifier { get; set; } = Guid.NewGuid().ToString();
    }
}
