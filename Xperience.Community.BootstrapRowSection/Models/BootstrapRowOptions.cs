using AngleSharp.Css.Dom;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xperience.Community.BootstrapRowSection.Models
{
    public class BootstrapRowOptions : IBootstrapRowOptions
    {
        public IEnumerable<DropDownOptionItem> BackgroundItems { get; set; } = Enumerable.Empty<DropDownOptionItem>();

        public BootstrapRowOptions SetupBackgroundItems(IEnumerable<DropDownOptionItem> items)
        {
            BackgroundItems = items;
            return this;
        }

        public BootstrapRowOptions SetupBackgroundItems(IEnumerable<string> items)
        {
            BackgroundItems = items.Select(x=>new DropDownOptionItem { Text = x, Value = x });
            return this;
        }
    }
}
