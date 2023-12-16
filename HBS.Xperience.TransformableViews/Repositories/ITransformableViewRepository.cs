using HBS.TransformableViews;
using HBS.Xperience.TransformableViews.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HBS.Xperience.TransformableViews.Repositories
{
    public interface ITransformableViewRepository
    {
        Dictionary<string, DateTime> LastViewedDates { get; set; }

        Task<IEnumerable<dynamic>> GetObjectItems(TransformableViewObjectsFormComponentModel model);
        ITransformableViewItem? GetTransformableView(string viewName, bool update = false);
        Task<IEnumerable<SelectListItem>> GetTransformableViewSelectItems();
    }
}