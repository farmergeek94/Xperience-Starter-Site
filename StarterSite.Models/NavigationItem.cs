namespace StarterSite.Models
{
    public class NavigationItem
    {
        public string Title { get; set; } = "";
        public string Url { get; set; } = "";
        public string UrlType { get; set; } = "";
        public IEnumerable<NavigationItem> Children { get; set;} = Enumerable.Empty<NavigationItem>();
        public int ID { get; set; }
        public int ParentID { get; set; }
    }
}