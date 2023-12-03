using CMS.DataEngine;
using HBS.Xperience.TransformableViews.Library;

namespace HBS.TransformableViews
{
    /// <summary>
    /// Class providing <see cref="TransformableViewInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(ITransformableViewInfoProvider))]
    internal partial class TransformableViewInfoProvider : AbstractInfoProvider<TransformableViewInfo, TransformableViewInfoProvider>, ITransformableViewInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransformableViewInfoProvider"/> class.
        /// </summary>
        public TransformableViewInfoProvider()
            : base(TransformableViewInfo.TYPEINFO)
        {
        }

        public override void Set(TransformableViewInfo info)
        {
            base.Set(info.EncryptContent());
        }
    }
}