using CMS.DataEngine;

namespace HBS.Xperience.Categories
{
    /// <summary>
    /// Class providing <see cref="ContentItemCategoryInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IContentItemCategoryInfoProvider))]
    public partial class ContentItemCategoryInfoProvider : AbstractInfoProvider<ContentItemCategoryInfo, ContentItemCategoryInfoProvider>, IContentItemCategoryInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentItemCategoryInfoProvider"/> class.
        /// </summary>
        public ContentItemCategoryInfoProvider()
            : base(ContentItemCategoryInfo.TYPEINFO)
        {
        }
    }
}