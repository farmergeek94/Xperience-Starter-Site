using AngleSharp.Css.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xperience.Accelerator.BootstrapRowSectionShared.Models
{
    public class BootstrapRowOptions : IBootstrapRowOptions
    {
        public IEnumerable<BootstrapOptionItem> BackgroundItems { get; set; } = Enumerable.Empty<BootstrapOptionItem>();

        public BootstrapRowOptions SetupBackgroundItems(IEnumerable<BootstrapOptionItem> items)
        {
            BackgroundItems = items;
            return this;
        }

        public BootstrapRowOptions SetupBackgroundItems(IEnumerable<string> items)
        {
            BackgroundItems = items.Select(x=>new BootstrapOptionItem { Text = x, Value = x });
            return this;
        }
    }
}
