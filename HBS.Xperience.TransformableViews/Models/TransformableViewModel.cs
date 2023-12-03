using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViews.Models
{
    public class TransformableViewModel
    {
        public string ViewTitle { get; set; } = "";
        public string ViewClassNames { get; set; } = "";
        public string ViewCustomContent { get; set; } = "";
        public IEnumerable<dynamic> Items { get; set; } = Enumerable.Empty<dynamic>();
    }
}
