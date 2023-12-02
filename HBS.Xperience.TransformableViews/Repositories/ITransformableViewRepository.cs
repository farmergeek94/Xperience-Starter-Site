using HBS.TransformableViews;

namespace HBS.Xperience.TransformableViews.Repositories
{
    internal interface ITransformableViewRepository
    {
        ITransformableViewItem? GetTransformableView(string viewName, bool update = false);
        IEnumerable<string> GetTransformableViewNames();
    }
}