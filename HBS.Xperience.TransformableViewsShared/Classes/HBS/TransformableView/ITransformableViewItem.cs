namespace HBS.TransformableViews
{
    public interface ITransformableViewItem
    {
        string TransformableViewContent { get; set; }
        string TransformableViewDisplayName { get; set; }
        Guid TransformableViewGuid { get; set; }
        int TransformableViewID { get; set; }
        DateTime TransformableViewLastModified { get; set; }
        string TransformableViewName { get; set; }
        int TransformableViewTransformableViewCategoryID { get; set; }
        int TransformableViewType { get; set; }
        string TransformableViewForm { get; set; }
    }
}