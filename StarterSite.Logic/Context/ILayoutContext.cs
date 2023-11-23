using CMS.Websites;

namespace StarterSite.Logic.Context
{
    public interface ILayoutContext
    {
        string? Title { get; set; }

        void Fill(IWebPageFieldsSource webPageFieldsSource);
    }
}