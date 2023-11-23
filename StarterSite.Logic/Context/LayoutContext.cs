using CMS.Websites;

namespace StarterSite.Logic.Context
{
    public class LayoutContext : ILayoutContext
    {
        public string? Title { get; set; } = null;
        public void Fill(IWebPageFieldsSource webPageFieldsSource)
        {
            Title = webPageFieldsSource.SystemFields.WebPageItemName;
        }
    }
}