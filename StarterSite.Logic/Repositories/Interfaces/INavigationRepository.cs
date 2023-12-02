namespace StarterSite.Logic.Repositories.Interfaces
{
    public interface INavigationRepository
    {
        Task<IEnumerable<NavigationItem>> GetNavigationItems(string path, string? channel = null);
    }
}