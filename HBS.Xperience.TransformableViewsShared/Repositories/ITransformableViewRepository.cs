using HBS.TransformableViews;
using HBS.Xperience.TransformableViewsShared.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HBS.Xperience.TransformableViewsShared.Repositories
{
    public interface ITransformableViewRepository
    {
        Dictionary<string, DateTime> LastViewedDates { get; set; }

        Task<IEnumerable<dynamic>> GetObjectItems(TransformableViewObjectsFormComponentModel model);
        ITransformableViewItem? GetTransformableView(string viewName, bool update = false);
        Task<IEnumerable<SelectListItem>> GetTransformableViewObjectSelectItems();
        Task<IEnumerable<SelectListItem>> GetTransformableViewSelectItems();
    }
}