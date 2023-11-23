namespace StarterSite.Logic.Repositories.Interfaces
{
    public interface IFaqRepository
    {
        Task<IEnumerable<FaqItem>> GetFaqs(IEnumerable<Guid>? groupIds = null, string language = "en");
        Task<IEnumerable<GroupItem>> GetFaqsGroups(IEnumerable<Guid> groupIds, string language = "en");
    }
}