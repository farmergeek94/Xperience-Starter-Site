using HBS.Xperience.TransformableViews.Models;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using Kentico.Xperience.Admin.Base.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViews.Components.FormComponents.TranformableViewFormComponent
{
    public class TransformableViewFormComponent : FormComponent<TransformableViewFormComponentProperties, TransformableViewFormComponentClientProperties, TransformableViewFormComponentModel>
    {
        public override string ClientComponentName => throw new NotImplementedException();
    }
    public class TransformableViewFormComponentProperties : FormComponentProperties
    {
    }

    public class TransformableViewFormComponentClientProperties : FormComponentClientProperties<TransformableViewFormComponentModel>
    {
    }

    public class TransformableViewFormComponentAttribute : FormComponentAttribute
    {
    }
}
