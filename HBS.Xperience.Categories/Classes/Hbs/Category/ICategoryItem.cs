namespace HBS.Xperience.Categories
{
    public interface ICategoryItem
    {
        string CategoryDisplayName { get; set; }
        Guid CategoryGuid { get; set; }
        int CategoryID { get; set; }
        DateTime CategoryLastModified { get; set; }
        string CategoryName { get; set; }
        int? CategoryParentID { get; set; }
        int CategoryOrder { get; set; }
    }
}