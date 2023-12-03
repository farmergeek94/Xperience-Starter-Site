using CMS.DataEngine;
using HBS.Xperience.TransformableViews.Models;
using HBS.Xperience.TransformableViews.Repositories;
using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using Kentico.Xperience.Admin.Base.Forms;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViews.Admin.FormComponents.TranformableViewFormComponent
{
    [ComponentAttribute(typeof(TransformableViewObjectsFormComponentAttribute))]
    public class TransformableViewObjectsFormComponent : FormComponent<TransformableViewObjectsFormComponentProperties, TransformableViewObjectsFormComponentClientProperties, TransformableViewObjectsFormComponentModel>
    {
        private readonly ITransformableViewRepository _transformableViewRepository;

        public override string ClientComponentName => "@hbs/xperience-transformable-views/TransformableViewObjects";

        public TransformableViewObjectsFormComponent(ITransformableViewRepository transformableViewRepository)
        {
            _transformableViewRepository = transformableViewRepository;
        }

        [FormComponentCommand]
        public async Task<ICommandResponse> GetViews()
        {
            IEnumerable<SelectListItem> views = await _transformableViewRepository.GetTransformableViewSelectItems();
            return ResponseFrom(views);
        }

        [FormComponentCommand]
        public async Task<ICommandResponse> GetObjectTypes()
        {
            var exisitingTypes = ObjectTypeManager.ExistingObjectTypes;
            var types = ObjectTypeManager.RegisteredTypes.Where(x => exisitingTypes.Contains(x.ObjectType)).Select(x => new SelectListItem(x.ObjectClassName, x.ObjectType));
            return ResponseFrom(types);
        }
    }
    public class TransformableViewObjectsFormComponentProperties : FormComponentProperties
    {
    }

    public class TransformableViewObjectsFormComponentClientProperties : FormComponentClientProperties<TransformableViewObjectsFormComponentModel>
    {
    }

    public class TransformableViewObjectsFormComponentAttribute : FormComponentAttribute
    {
    }
}
