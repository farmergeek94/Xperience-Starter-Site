using CMS.DataEngine;
using HBS.Xperience.TransformableViewsShared.Library;
using HBS.Xperience.TransformableViewsShared.Models;
using HBS.Xperience.TransformableViewsShared.Repositories;
using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using Kentico.Xperience.Admin.Base.Forms;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViewsAdmin.Admin.FormComponents.TranformableViewFormComponent
{
    [ComponentAttribute(typeof(TransformableViewWidgetFormComponentAttribute))]
    public class TransformableViewWidgetFormComponent : FormComponent<TransformableViewWidgetFormComponentClientProperties, TransformableViewWidgetFormComponentModel>
    {
        private readonly ITransformableViewRepository _transformableViewRepository;

        public override string ClientComponentName => "@hbs/xperience-transformable-views/TransformableViewWidget";

        public TransformableViewWidgetFormComponent(ITransformableViewRepository transformableViewRepository)
        {
            _transformableViewRepository = transformableViewRepository;
        }

        [FormComponentCommand]
        public async Task<ICommandResponse> GetViews()
        {
            IEnumerable<SelectListItem> views = await _transformableViewRepository.GetTransformableViewSelectItems();
            return ResponseFrom(views);
        }
    }

    public class TransformableViewWidgetFormComponentClientProperties : FormComponentClientProperties<TransformableViewWidgetFormComponentModel>
    {
    }
}
