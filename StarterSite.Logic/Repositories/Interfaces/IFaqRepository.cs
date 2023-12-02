namespace StarterSite.Logic.Repositories.Interfaces
{
    public interface IFaqRepository
    {
        Task<IEnumerable<FaqItem>> GetFaqs(IEnumerable<int>? categories = null, string language = "en");
        Task<IEnumerable<FaqItem>> GetFaqsByCategory(IEnumerable<int> categories, string language = "en");
        Task<IEnumerable<GroupItem>> GetFaqsGroups(IEnumerable<int> categories, string language = "en");
    }
}