namespace StarterSite.Models
{
    public class GroupItem
    {
        public string Title { get; set; }
        public IEnumerable<FaqItem> Faqs { get; set; }
    }
}