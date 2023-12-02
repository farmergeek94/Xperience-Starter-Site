using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViews.Models
{
    public class TransformableViewModel
    {
        public string ContainerTitle { get; set; } = "";
        public string ContainerCSS { get; set; } = "";
        public string ContainerCustomContent { get; set; } = "";
        public IEnumerable<dynamic> Items { get; set; } = Enumerable.Empty<dynamic>();
    }
}
