using HBS.Xperience.TransformableViews.Admin.FormComponents.TranformableViewFormComponent;
using HBS.Xperience.TransformableViews.Components;
using HBS.Xperience.TransformableViews.Models;
using HBS.Xperience.TransformableViews.Repositories;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: RegisterWidget(
    identifier: "HBS.TransformableViewObjects",
    customViewName: "~/Components/_TransformableViewObjects.cshtml",
    name: "Transformable View Objects",
    propertiesType: typeof(TransformableViewObjectsWidgetProperties),
    IconClass = "icon-layout")]

namespace HBS.Xperience.TransformableViews.Components
{
    public class TransformableViewObjectsWidgetViewComponent : ViewComponent
    {
        private readonly ITransformableViewRepository _transformableViewRepository;

        public TransformableViewObjectsWidgetViewComponent(ITransformableViewRepository transformableViewRepository)
        {
            _transformableViewRepository = transformableViewRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(TransformableViewObjectsFormComponentModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.ClassName))
            {
                var items = await _transformableViewRepository.GetObjectItems(model);

                var viewModel = new TransformableViewModel()
                {
                    ViewTitle = model.ViewTitle,
                    ViewClassNames = model.ViewClassNames,
                    ViewCustomContent = model.ViewCustomContent,
                    Items = items
                };
                return View(model.View, viewModel);
                //return Content(string.Empty);
            }
            return Content(string.Empty);
        }
    }

    public class TransformableViewObjectsWidgetProperties : IWidgetProperties
    {
        [TransformableViewObjectsFormComponent]
        public TransformableViewObjectsFormComponentModel Model { get; set; } = new TransformableViewObjectsFormComponentModel();
    }
}
