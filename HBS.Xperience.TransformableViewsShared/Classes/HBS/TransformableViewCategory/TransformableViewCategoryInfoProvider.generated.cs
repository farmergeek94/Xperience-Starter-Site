using CMS.DataEngine;

namespace HBS.TransformableViews_Experience
{
    /// <summary>
    /// Class providing <see cref="TransformableViewCategoryInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(ITransformableViewCategoryInfoProvider))]
    public partial class TransformableViewCategoryInfoProvider : AbstractInfoProvider<TransformableViewCategoryInfo, TransformableViewCategoryInfoProvider>, ITransformableViewCategoryInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransformableViewCategoryInfoProvider"/> class.
        /// </summary>
        public TransformableViewCategoryInfoProvider()
            : base(TransformableViewCategoryInfo.TYPEINFO)
        {
        }
    }
}