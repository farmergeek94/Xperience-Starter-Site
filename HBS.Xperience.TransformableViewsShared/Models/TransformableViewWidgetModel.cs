using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViewsShared.Models
{
    public class TransformableViewWidgetModel : TransformableViewModelBase
    {
        public ExpandoObject Inputs { get; set; } = new ExpandoObject();
    }
}
