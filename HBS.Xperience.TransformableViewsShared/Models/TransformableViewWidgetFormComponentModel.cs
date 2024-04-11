using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViewsShared.Models
{
    public class TransformableViewInput
    {
        public string Type { get; set; } = "text";
        public string Name { get; set; } = "";
        public object Value { get; set; }
    }
    public class TransformableViewWidgetFormComponentModel : TransformableViewModelBase
    {
        public List<TransformableViewInput> TransformableInputs { get; set; } = new List<TransformableViewInput>();// Enumerable.Empty<TransformableViewInput>();
        public string View {  get; set; } = string.Empty;

    }
}
