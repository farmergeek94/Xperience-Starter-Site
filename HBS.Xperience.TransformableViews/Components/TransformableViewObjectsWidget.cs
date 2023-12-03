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
    viewComponentType: typeof(TransformableViewObjectsWidget),
    name: "Transformable View Objects",
    propertiesType: typeof(TransformableViewObjectsWidgetProperties))]

namespace HBS.Xperience.TransformableViews.Components
{
    public class TransformableViewObjectsWidget : ViewComponent
    {
        private readonly ITransformableViewRepository _transformableViewRepository;

        public TransformableViewObjectsWidget(ITransformableViewRepository transformableViewRepository)
        {
            _transformableViewRepository = transformableViewRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(ComponentViewModel<TransformableViewObjectsWidgetProperties> widgetModel)
        {
            if (!string.IsNullOrWhiteSpace(widgetModel.Properties.Model.ClassName))
            {
                var items = await _transformableViewRepository.GetObjectItems(widgetModel.Properties.Model);

                var model = new TransformableViewModel()
                {
                    ViewTitle = widgetModel.Properties.Model.ViewTitle,
                    ViewClassNames = widgetModel.Properties.Model.ViewClassNames,
                    ViewCustomContent = widgetModel.Properties.Model.ViewCustomContent,
                    Items = items
                };
                return View(widgetModel.Properties.Model.View, model);
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
