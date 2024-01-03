namespace HBS.TransformableViews
{
    public interface ITransformableViewCategoryItem
    {
        string TransformableViewCategoryDisplayName { get; set; }
        Guid TransformableViewCategoryGuid { get; set; }
        int TransformableViewCategoryID { get; set; }
        DateTime TransformableViewCategoryLastModified { get; set; }
        string TransformableViewCategoryName { get; set; }
        int TransformableViewCategoryOrder { get; set; }
        int? TransformableViewCategoryParentID { get; set; }
    }
}