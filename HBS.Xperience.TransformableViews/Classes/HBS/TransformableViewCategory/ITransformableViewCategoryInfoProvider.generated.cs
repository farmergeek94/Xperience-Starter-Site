using CMS.DataEngine;

namespace HBS.TransformableViews
{
    /// <summary>
    /// Declares members for <see cref="TransformableViewCategoryInfo"/> management.
    /// </summary>
    public partial interface ITransformableViewCategoryInfoProvider : IInfoProvider<TransformableViewCategoryInfo>, IInfoByIdProvider<TransformableViewCategoryInfo>, IInfoByNameProvider<TransformableViewCategoryInfo>, IInfoByGuidProvider<TransformableViewCategoryInfo>
    {
    }
}