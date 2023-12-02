using Kentico.Xperience.Admin.Base.FormAnnotations;
using Kentico.Xperience.Admin.Base.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.Categories.Admin.FormComponents
{
    [ComponentAttribute(typeof(CategoryListFormComponentAttribute))]
    public class CategoryListFormComponent : FormComponent<CategoryListFormComponentProperties, CategoryListFormComponentClientProperties, IEnumerable<int>>
    {
        private readonly ICategoryInfoProvider _categoryInfoProvider;

        public CategoryListFormComponent(ICategoryInfoProvider categoryInfoProvider)
        {
            _categoryInfoProvider = categoryInfoProvider;
        }
        public override string ClientComponentName => "@hbs/xperience-categories/CategoryList";

        [FormComponentCommand]
        public async Task<ICommandResponse> GetCategories()
        {
            IEnumerable<ICategoryItem> categories = await _categoryInfoProvider.Get().GetEnumerableTypedResultAsync();
            return ResponseFrom(categories);
        }
    }
    public class CategoryListFormComponentProperties : FormComponentProperties
    {

    }

    public class CategoryListFormComponentClientProperties : FormComponentClientProperties<IEnumerable<int>>
    {
    }

    public class CategoryListFormComponentAttribute : FormComponentAttribute
    {
    }
}
