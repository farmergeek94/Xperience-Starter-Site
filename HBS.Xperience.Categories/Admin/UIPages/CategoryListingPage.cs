using CMS.Helpers;
using HBS.Xperience.Categories.Admin.UIPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: UIPage(typeof(CategoryApplicationPage), "hbs-category-list", typeof(CategoryListingPage), "Category List", CategoryListingPage.TemplateName, 0)]
namespace HBS.Xperience.Categories.Admin.UIPages
{
    public class CategoryListingPage : Page<CategoryListingPageemplateClientProperties>
    {
        public const string TemplateName = "@hbs/xperience-categories/CategoryListPage"; 

        private readonly ICategoryInfoProvider _categoryInfoProvider;

        public CategoryListingPage(ICategoryInfoProvider categoryInfoProvider)
        {
            _categoryInfoProvider = categoryInfoProvider;
        }

        public override async Task<CategoryListingPageemplateClientProperties> ConfigureTemplateProperties(CategoryListingPageemplateClientProperties properties)
        {
            properties.Categories = await _categoryInfoProvider.Get().GetEnumerableTypedResultAsync();
            return properties;
        }

        [PageCommand]
        public async Task<ICommandResponse> SetCategory(CategoryModel category)
        {
            CategoryInfo categoryInfo;
            if (category.CategoryID == null)
            {
                categoryInfo = new CategoryInfo { 
                    CategoryDisplayName = category.CategoryDisplayName,
                    CategoryName = ValidationHelper.GetCodeName(category.CategoryDisplayName),
                    CategoryParentID = category.CategoryParentID,
                    CategoryOrder = category.CategoryOrder,
                };
                // Add guid if it fails.
                try
                {
                    _categoryInfoProvider.Set(categoryInfo);
                }
                catch
                {
                    categoryInfo.CategoryName = categoryInfo.CategoryName + "_" + Guid.NewGuid().ToString();
                    _categoryInfoProvider.Set(categoryInfo);
                }
            } else
            {
                categoryInfo = await _categoryInfoProvider.GetAsync(category.CategoryID.Value);
                categoryInfo.CategoryDisplayName = category.CategoryDisplayName;
                categoryInfo.CategoryParentID = category.CategoryParentID;
                categoryInfo.CategoryOrder = category.CategoryOrder;
                _categoryInfoProvider.Set(categoryInfo);
            }
            return ResponseFrom((ICategoryItem)categoryInfo).AddSuccessMessage("Category Saved Successfully");
        }

        [PageCommand]
        public async Task<ICommandResponse> SetCategories(CategoryModel[] categories)
        {
            var ids = categories.Select(x => x.CategoryID ?? 0);
            var categoryList = await _categoryInfoProvider.Get().Where(x => x.WhereIn(nameof(CategoryInfo.CategoryID), ids.ToArray())).GetEnumerableTypedResultAsync();
            var returnCategories = new List<ICategoryItem>();
            foreach (var category in categories)
            {
                CategoryInfo categoryInfo;
                if (category.CategoryID == null)
                {
                    categoryInfo = new CategoryInfo
                    {
                        CategoryDisplayName = category.CategoryDisplayName,
                        CategoryName = ValidationHelper.GetCodeName(category.CategoryDisplayName),
                        CategoryParentID = category.CategoryParentID,
                        CategoryOrder = category.CategoryOrder,
                    };
                    try
                    {
                        _categoryInfoProvider.Set(categoryInfo);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    categoryInfo = categoryList.First(x => x.CategoryID == category.CategoryID);
                    categoryInfo.CategoryDisplayName = category.CategoryDisplayName;
                    categoryInfo.CategoryParentID = category.CategoryParentID;
                    categoryInfo.CategoryOrder = category.CategoryOrder;
                    _categoryInfoProvider.Set(categoryInfo);
                }
                returnCategories.Add(categoryInfo);
            }
            return ResponseFrom(returnCategories).AddSuccessMessage("Category Saved Successfully");
        }

        [PageCommand]
        public async Task<ICommandResponse> DeleteCategory(int categoryID)
        {      
            var categoryInfo = await _categoryInfoProvider.GetAsync(categoryID);
            _categoryInfoProvider.Delete(categoryInfo);
            return ResponseFrom(categoryID).AddSuccessMessage("Category Deleted Successfully");
        }
    }

    public class CategoryModel
    {
        public string CategoryDisplayName { get; set; }
        public Guid? CategoryGuid { get; set; }
        public int? CategoryID { get; set; }
        public DateTime? CategoryLastModified { get; set; }
        public string? CategoryName { get; set; }
        public int? CategoryParentID { get; set; }
        public int CategoryOrder { get; set; }
    }

    // Contains properties that match the properties of the client template
    // Specify such classes as the generic parameter of Page<TClientProperties>
    public class CategoryListingPageemplateClientProperties : TemplateClientProperties
    {
        // For example
        public IEnumerable<ICategoryItem> Categories { get; set; } = Enumerable.Empty<ICategoryItem>();
    }
}
