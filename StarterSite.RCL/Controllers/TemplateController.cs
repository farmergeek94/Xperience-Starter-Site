using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc.PageTemplates;
using StarterSite.RCL.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X;

[assembly: RegisterWebPageRoute(
    contentTypeName: Page.CONTENT_TYPE_NAME,
    controllerType: typeof(TemplateController))]

namespace StarterSite.RCL.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
            return new TemplateResult();
        }
    }
}