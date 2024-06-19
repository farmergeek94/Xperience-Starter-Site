using CMS.Helpers;
using HBS.TransformableViews;
using HBS.TransformableViews_Experience;
using HBS.Xperience.TransformableViewsAdmin.Admin.UIPages;
using HBS.Xperience.TransformableViewsShared.Services;
using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.UIPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

[assembly: UIPage(typeof(TransformableViewApplicationPage), "hbs-transformable-view-editor", typeof(TransformableViewPage), "Transformable View Editor", TransformableViewPage.TemplateName, 0)]
namespace HBS.Xperience.TransformableViewsAdmin.Admin.UIPages
{
    internal class TransformableViewPage : Page<TransformableViewPageClientProperties>
    {
        public const string TemplateName = "@hbs/xperience-transformable-views/TransformableViewPage"; 

        private readonly ITransformableViewCategoryInfoProvider _transformableViewCategoryInfoProvider;
        private readonly ITransformableViewInfoProvider _transformableViewInfoProvider;
        private readonly IEncryptionService _encryptionService;

        public TransformableViewPage(ITransformableViewCategoryInfoProvider categoryInfoProvider, ITransformableViewInfoProvider transformableViewInfoProvider, IEncryptionService encryptionService)
        {
            _transformableViewCategoryInfoProvider = categoryInfoProvider;
            _transformableViewInfoProvider = transformableViewInfoProvider;
            _encryptionService = encryptionService;
        }

        public override async Task<TransformableViewPageClientProperties> ConfigureTemplateProperties(TransformableViewPageClientProperties properties)
        {
            properties.Categories = await _transformableViewCategoryInfoProvider.Get().GetEnumerableTypedResultAsync();
            return properties;
        }

        [PageCommand]
        public async Task<ICommandResponse> SetCategory(TransformableViewCategoryModel category)
        {
            TransformableViewCategoryInfo categoryInfo;
            if (category.TransformableViewCategoryID == null)
            {
                categoryInfo = new TransformableViewCategoryInfo
                { 
                    TransformableViewCategoryDisplayName = category.TransformableViewCategoryDisplayName,
                    TransformableViewCategoryName = ValidationHelper.GetCodeName(category.TransformableViewCategoryDisplayName),
                    TransformableViewCategoryParentID = category.TransformableViewCategoryParentID,
                    TransformableViewCategoryOrder = category.TransformableViewCategoryOrder,
                };
                // Add guid if it fails.
                try
                {
                    _transformableViewCategoryInfoProvider.Set(categoryInfo);
                }
                catch
                {
                    categoryInfo.TransformableViewCategoryName = categoryInfo.TransformableViewCategoryName + "_" + Guid.NewGuid().ToString();
                    _transformableViewCategoryInfoProvider.Set(categoryInfo);
                }
            } else
            {
                categoryInfo = await _transformableViewCategoryInfoProvider.GetAsync(category.TransformableViewCategoryID.Value);
                categoryInfo.TransformableViewCategoryDisplayName = category.TransformableViewCategoryDisplayName;
                categoryInfo.TransformableViewCategoryParentID = category.TransformableViewCategoryParentID;
                categoryInfo.TransformableViewCategoryOrder = category.TransformableViewCategoryOrder;
                _transformableViewCategoryInfoProvider.Set(categoryInfo);
            }
            return ResponseFrom((ITransformableViewCategoryItem)categoryInfo).AddSuccessMessage("Category Saved Successfully");
        }

        [PageCommand]
        public async Task<ICommandResponse> SetCategories(TransformableViewCategoryModel[] categories)
        {
            var ids = categories.Select(x => x.TransformableViewCategoryID ?? 0);
            var categoryList = await _transformableViewCategoryInfoProvider.Get().Where(x => x.WhereIn(nameof(TransformableViewCategoryInfo.TransformableViewCategoryID), ids.ToArray())).GetEnumerableTypedResultAsync();
            var returnCategories = new List<ITransformableViewCategoryItem>();
            foreach (var category in categories)
            {
                TransformableViewCategoryInfo? categoryInfo = categoryList.FirstOrDefault(x => x.TransformableViewCategoryID == category.TransformableViewCategoryID);
                if (categoryInfo != null)
                {
                    categoryInfo.TransformableViewCategoryDisplayName = category.TransformableViewCategoryDisplayName;
                    categoryInfo.TransformableViewCategoryParentID = category.TransformableViewCategoryParentID;
                    categoryInfo.TransformableViewCategoryOrder = category.TransformableViewCategoryOrder;
                    _transformableViewCategoryInfoProvider.Set(categoryInfo);
                    returnCategories.Add(categoryInfo);
                }
            }
            return ResponseFrom(returnCategories).AddSuccessMessage("Category Saved Successfully");
        }

        [PageCommand]
        public async Task<ICommandResponse> DeleteCategory(int categoryID)
        {      
            var categoryInfo = await _transformableViewCategoryInfoProvider.GetAsync(categoryID);
            _transformableViewCategoryInfoProvider.Delete(categoryInfo);
            return ResponseFrom(categoryID).AddSuccessMessage("Category Deleted Successfully");
        }

        [PageCommand]
        public async Task<ICommandResponse> GetViews(int categoryID)
        {
            IEnumerable<ITransformableViewItem> views = await _transformableViewInfoProvider.Get().Where(x => x.WhereEquals(nameof(ITransformableViewItem.TransformableViewTransformableViewCategoryID), categoryID)).GetEnumerableTypedResultAsync();
            foreach(var view in views)
            {
                view.TransformableViewContent = _encryptionService.DecryptString(view.TransformableViewContent);
            }
            return ResponseFrom(views.DeSerializeForm());
        }

        [PageCommand]
        public async Task<ICommandResponse> SetView(TransformableViewModel model)
        {
            if (model.TransformableViewID != null)
            {
                TransformableViewInfo view = await _transformableViewInfoProvider.GetAsync(model.TransformableViewID.Value);
                if (view != null)
                {
                    view.TransformableViewDisplayName = model.TransformableViewDisplayName;
                    view.TransformableViewContent = model.TransformableViewContent;
                    view.TransformableViewType = model.TransformableViewType;
                    view.TransformableViewForm = model.TransformableViewForm != null ? JsonSerializer.Serialize(model.TransformableViewForm) : string.Empty;
                    _transformableViewInfoProvider.Set(view);
                    return ResponseFrom((ITransformableViewItem)view).AddSuccessMessage("View saved");
                }
                return Response().AddErrorMessage("View not found");
            } else
            {
                TransformableViewInfo view = new()
                {
                    TransformableViewDisplayName = model.TransformableViewDisplayName,
                    TransformableViewContent = model.TransformableViewContent,
                    TransformableViewName = ValidationHelper.GetCodeName(model.TransformableViewDisplayName),
                    TransformableViewTransformableViewCategoryID = model.TransformableViewTransformableViewCategoryID,
                    TransformableViewType = model.TransformableViewType,
                    TransformableViewForm = model.TransformableViewForm != null ? JsonSerializer.Serialize(model.TransformableViewForm) : string.Empty
            };

                // Add guid if it fails.
                try
                {
                    _transformableViewInfoProvider.Set(view);
                }
                catch
                {
                    view.TransformableViewName = view.TransformableViewName + "_" + Guid.NewGuid().ToString();
                    // reset the content to avoid double encryption.  
                    view.TransformableViewContent = view.TransformableViewContent;
                    _transformableViewInfoProvider.Set(view);
                }
                return ResponseFrom((ITransformableViewItem)view).AddSuccessMessage("View creaeted");
            }
        }

        [PageCommand]
        public async Task<ICommandResponse> SetViews(IEnumerable<TransformableViewModel> model)
        {
            var ids = model.Where(x=>x.TransformableViewID.HasValue).Select(x => x.TransformableViewID.Value);
            var viewList = new List<ITransformableViewItem>();
            if (ids.Any())
            {
                var views = await _transformableViewInfoProvider.Get().Where(w =>
                    w.WhereIn(nameof(ITransformableViewItem.TransformableViewID), ids.ToArray())
                ).GetEnumerableTypedResultAsync();
                foreach(var viewModel in model) {
                    var view = views.First(x => x.TransformableViewID == viewModel.TransformableViewID);
                    view.TransformableViewDisplayName = viewModel.TransformableViewDisplayName;
                    view.TransformableViewContent = viewModel.TransformableViewContent;
                    view.TransformableViewType = viewModel.TransformableViewType;
                    view.TransformableViewForm = viewModel.TransformableViewForm != null ? JsonSerializer.Serialize(viewModel.TransformableViewForm) : string.Empty;
                    _transformableViewInfoProvider.Set(view);
                    viewList.Add(view);
                }
            }
            return ResponseFrom(viewList);
        }

        [PageCommand]
        public async Task<ICommandResponse> DeleteView(int viewID)
        {
            var viewInfo = await _transformableViewInfoProvider.GetAsync(viewID);
            _transformableViewInfoProvider.Delete(viewInfo);
            return ResponseFrom(viewID).AddSuccessMessage("Category Deleted Successfully");
        }
    }

    public class TransformableViewCategoryModel
    {
        public string TransformableViewCategoryDisplayName { get; set; }
        public Guid? TransformableViewCategoryGuid { get; set; }
        public int? TransformableViewCategoryID { get; set; }
        public DateTime? TransformableViewCategoryLastModified { get; set; }
        public string? TransformableViewCategoryName { get; set; }
        public int? TransformableViewCategoryParentID { get; set; }
        public int TransformableViewCategoryOrder { get; set; }
    }

    public class TransformableViewModel
    {
        public string TransformableViewContent { get; set; }
        public string TransformableViewDisplayName { get; set; }
        public Guid? TransformableViewGuid { get; set; }
        public int? TransformableViewID { get; set; }
        public DateTime? TransformableViewLastModified { get; set; }
        public DateTime? TransformableViewLastRequested { get; set; }
        public string? TransformableViewName { get; set; }
        public int TransformableViewTransformableViewCategoryID { get; set; }
        public int TransformableViewType { get; set; } = 0;
        public IEnumerable<TransformableViewFormItem>? TransformableViewForm { get; set; }
    }

    public class TransformableViewFormItem
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }



    // Contains properties that match the properties of the client template
    // Specify such classes as the generic parameter of Page<TClientProperties>
    public class TransformableViewPageClientProperties : TemplateClientProperties
    {
        // For example
        public IEnumerable<ITransformableViewCategoryItem> Categories { get; set; } = Enumerable.Empty<ITransformableViewCategoryItem>();
    }

    public static class TransformableViewPageHelper {
        public static IEnumerable<TransformableViewModel> DeSerializeForm(this IEnumerable<ITransformableViewItem> items)
        {
            foreach (var item in items)
            {
                TransformableViewModel nItem = new()
                {
                    TransformableViewContent = item.TransformableViewContent,
                    TransformableViewDisplayName = item.TransformableViewDisplayName,
                    TransformableViewGuid = item.TransformableViewGuid,
                    TransformableViewID = item.TransformableViewID,
                    TransformableViewLastModified = item.TransformableViewLastModified,
                    TransformableViewName = item.TransformableViewName,
                    TransformableViewTransformableViewCategoryID = item.TransformableViewTransformableViewCategoryID,
                    TransformableViewType = item.TransformableViewType,
                    TransformableViewForm = !string.IsNullOrWhiteSpace(item.TransformableViewForm) ? JsonSerializer.Deserialize<IEnumerable<TransformableViewFormItem>>(item.TransformableViewForm) : null 
                };
                yield return nItem;
            }
        }
    }
}
