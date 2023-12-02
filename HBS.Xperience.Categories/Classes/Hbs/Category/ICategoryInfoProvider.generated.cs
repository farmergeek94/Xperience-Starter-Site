using CMS.DataEngine;

namespace HBS.Xperience.Categories
{
    /// <summary>
    /// Declares members for <see cref="CategoryInfo"/> management.
    /// </summary>
    public partial interface ICategoryInfoProvider : IInfoProvider<CategoryInfo>, IInfoByIdProvider<CategoryInfo>, IInfoByNameProvider<CategoryInfo>, IInfoByGuidProvider<CategoryInfo>
    {
    }
}