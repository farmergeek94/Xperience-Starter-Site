namespace StarterSite.Models
{
    public class WebPageItem
    {
        public WebPageItem()
        {
        }

        public Guid WebPageItemGUID { get; set; }
        public int WebPageItemID { get; set; }
        public string WebPageItemName { get; set; }
        public int WebPageItemOrder { get; set; }
        public int WebPageItemParentID { get; set; }
        public string WebPageItemTreePath { get; set; }
        public string WebPageUrlPath { get; set; }
        public string WebPageRelativeUrl { get; set; }
    }

    public class WebPageItem<T> : WebPageItem where T : new()
    {
        public T Data { get; set; }
    }
}