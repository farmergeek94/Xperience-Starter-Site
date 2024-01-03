using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViewsShared.Models
{
    public class TransformableViewWidgetModel
    {
        public string ViewTitle { get; set; } = "";
        public string ViewClassNames { get; set; } = "";
        public string ViewCustomContent { get; set; } = "";
        public ExpandoObject Inputs { get; set; } = new ExpandoObject();
    }
}
