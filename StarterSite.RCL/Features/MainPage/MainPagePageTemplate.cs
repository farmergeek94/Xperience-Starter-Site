using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc.PageTemplates;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using StarterSite.RCL.Features.MainPageTemplate;
using X;

//[assembly: RegisterPageTemplate(
//    "Generic.MainPage_Default",
//    "Main Page",
//    typeof(MainPagePageTemplateProperties),
//    "~/Features/MainPage/MainPagePageTemplate.cshtml")]

namespace StarterSite.RCL.Features.MainPageTemplate
{
    public class MainPagePageTemplateProperties : IPageTemplateProperties
    {
        [CheckBoxComponent(Label = "Full Width")]
        public bool FullWidth { get; set; } = false;
    }
}
