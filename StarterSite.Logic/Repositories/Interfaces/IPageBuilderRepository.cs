using Kentico.Content.Web.Mvc;

namespace StarterSite.Logic.Repositories.Interfaces
{
    public interface IPageBuilderRepository
    {
        bool IsPreview { get; }
        string Language { get; }
        RoutedWebPage PageContext { get; }
    }
}