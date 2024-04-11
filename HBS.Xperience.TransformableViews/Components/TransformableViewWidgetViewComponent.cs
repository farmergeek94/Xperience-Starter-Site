using CMS.Helpers;
using HBS.Xperience.TransformableViews.Components;
using HBS.Xperience.TransformableViewsShared.Library;
using HBS.Xperience.TransformableViewsShared.Models;
using HBS.Xperience.TransformableViewsShared.Repositories;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: RegisterWidget(
    identifier: TransformableViewWidgetViewComponent.Identifier,
    customViewName: "~/Components/_TransformableViewWidget.cshtml",
    name: "Transformable View Widget",
    propertiesType: typeof(TransformableViewWidgetProperties),
    IconClass = "icon-braces-octothorpe")]

namespace HBS.Xperience.TransformableViews.Components
{
    public class TransformableViewWidgetViewComponent : ViewComponent
    {
        public const string Identifier = "HBS.TransformableViewWidgets";

        private readonly ITransformableViewRepository _transformableViewRepository;

        public TransformableViewWidgetViewComponent(ITransformableViewRepository transformableViewRepository)
        {
            _transformableViewRepository = transformableViewRepository;
        }
        public IViewComponentResult Invoke(TransformableViewWidgetFormComponentModel model)
        {
            if (model.TransformableInputs.Any())
            {
                var eOb = new ExpandoObject() as IDictionary<string, object?>;
                foreach(var input in model.TransformableInputs)
                {
                    var codeName = ValidationHelper.GetCodeName(input.Name);
                    eOb.Add(codeName, input.Value);
                }
                var viewModel = new TransformableViewWidgetModel()
                {
                    ViewTitle = model.ViewTitle,
                    ViewClassNames = model.ViewClassNames,
                    ViewCustomContent = model.ViewCustomContent,
                    Inputs = (ExpandoObject)eOb
                };
                return View(model.View, viewModel);
            }
            return Content(string.Empty);
        }
    }

    public class TransformableViewWidgetProperties : IWidgetProperties
    {
        [TransformableViewWidgetFormComponent]
        public TransformableViewWidgetFormComponentModel Model { get; set; } = new TransformableViewWidgetFormComponentModel();
    }
}
