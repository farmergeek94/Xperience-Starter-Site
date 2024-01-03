using CMS.DataEngine;

namespace HBS.TransformableViews
{
    /// <summary>
    /// Declares members for <see cref="TransformableViewInfo"/> management.
    /// </summary>
    public partial interface ITransformableViewInfoProvider : IInfoProvider<TransformableViewInfo>, IInfoByIdProvider<TransformableViewInfo>, IInfoByNameProvider<TransformableViewInfo>, IInfoByGuidProvider<TransformableViewInfo>
    {
    }
}