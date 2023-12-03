using HBS.TransformableViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViews.Library
{
    public static class ExtensionMethods
    {
        public static TransformableViewInfo EncryptContent(this TransformableViewInfo viewInfo)
        {
            if (!string.IsNullOrWhiteSpace(viewInfo.TransformableViewContent)) {
                viewInfo.TransformableViewContent = EncryptionService.EncryptString(viewInfo.TransformableViewContent);
            }
            return viewInfo;
        }

        public static TransformableViewInfo DecryptContent(this TransformableViewInfo viewInfo)
        {
            if (!string.IsNullOrWhiteSpace(viewInfo.TransformableViewContent))
            {
                viewInfo.TransformableViewContent = EncryptionService.DecryptString(viewInfo.TransformableViewContent);
            }
            return viewInfo;
        }
    }
}
