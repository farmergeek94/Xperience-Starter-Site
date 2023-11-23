using CMS.Websites;
using StarterSite.Models;

namespace StarterSite.Logic.Repositories.Interfaces
{
    public interface IWebPageRepository
    {
        Task<WebPageItem<TI>> GetCurrentPageItem<T, TI>(Func<T, TI> dataMapper)
            where T : IWebPageFieldsSource, new()
            where TI : new();
        Task<WebPageItem<TI>> GetPageItem<T, TI>(int id, Func<T, TI> dataMapper, string language = "en", bool ForPreview = false)
            where T : IWebPageFieldsSource, new()
            where TI : new();
    }
}