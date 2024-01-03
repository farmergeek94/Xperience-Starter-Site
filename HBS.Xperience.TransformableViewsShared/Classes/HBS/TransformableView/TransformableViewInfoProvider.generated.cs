using CMS.Core;
using CMS.DataEngine;
using HBS.Xperience.TransformableViewsShared.Services;

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
            var service = Service.Resolve<IEncryptionService>();
            base.Set(service.EncryptView(info));
        }
    }
}