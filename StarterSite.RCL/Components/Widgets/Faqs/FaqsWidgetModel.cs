using StarterSite.Models;

namespace StarterSite.RCL.Components.Widgets.Faqs
{
    internal class FaqsWidgetModel
    {
        public bool ShowGroupHeaders { get; set; }
        public IEnumerable<GroupItem> Groups { get; set; }
    }
}