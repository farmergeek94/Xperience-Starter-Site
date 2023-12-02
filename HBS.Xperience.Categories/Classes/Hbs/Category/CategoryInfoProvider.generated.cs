using CMS.DataEngine;

namespace HBS.Xperience.Categories
{
    /// <summary>
    /// Class providing <see cref="CategoryInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(ICategoryInfoProvider))]
    public partial class CategoryInfoProvider : AbstractInfoProvider<CategoryInfo, CategoryInfoProvider>, ICategoryInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryInfoProvider"/> class.
        /// </summary>
        public CategoryInfoProvider()
            : base(CategoryInfo.TYPEINFO)
        {
        }
    }
}