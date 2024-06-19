using System;
using System.Data;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using HBS.TransformableViews_Experience;
using HBS.TransformableViews;

[assembly: RegisterObjectType(typeof(TransformableViewCategoryInfo), TransformableViewCategoryInfo.OBJECT_TYPE)]

namespace HBS.TransformableViews_Experience
{
    /// <summary>
    /// Data container class for <see cref="TransformableViewCategoryInfo"/>.
    /// </summary>
    [Serializable, InfoCache(InfoCacheBy.ID | InfoCacheBy.Name | InfoCacheBy.Guid)]
    public partial class TransformableViewCategoryInfo : AbstractInfo<TransformableViewCategoryInfo, ITransformableViewCategoryInfoProvider>, IInfoWithId, IInfoWithName, IInfoWithGuid, ITransformableViewCategoryItem
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "hbs.transformableviewcategory";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(TransformableViewCategoryInfoProvider), OBJECT_TYPE, "HBS.TransformableViewCategory", "TransformableViewCategoryID", "TransformableViewCategoryLastModified", "TransformableViewCategoryGuid", "TransformableViewCategoryName", "TransformableViewCategoryDisplayName", null, null, "hbs.transformableviewcategory")
        {
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Transformable view category ID.
        /// </summary>
        [DatabaseField]
        public virtual int TransformableViewCategoryID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(TransformableViewCategoryID)), 0);
            set => SetValue(nameof(TransformableViewCategoryID), value);
        }


        /// <summary>
        /// Transformable view category guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid TransformableViewCategoryGuid
        {
            get => ValidationHelper.GetGuid(GetValue(nameof(TransformableViewCategoryGuid)), Guid.Empty);
            set => SetValue(nameof(TransformableViewCategoryGuid), value);
        }


        /// <summary>
        /// Transformable view category name.
        /// </summary>
        [DatabaseField]
        public virtual string TransformableViewCategoryName
        {
            get => ValidationHelper.GetString(GetValue(nameof(TransformableViewCategoryName)), String.Empty);
            set => SetValue(nameof(TransformableViewCategoryName), value);
        }


        /// <summary>
        /// Transformable view category display name.
        /// </summary>
        [DatabaseField]
        public virtual string TransformableViewCategoryDisplayName
        {
            get => ValidationHelper.GetString(GetValue(nameof(TransformableViewCategoryDisplayName)), String.Empty);
            set => SetValue(nameof(TransformableViewCategoryDisplayName), value);
        }


        /// <summary>
        /// Transformable view category parent ID.
        /// </summary>
        [DatabaseField]
        public virtual int? TransformableViewCategoryParentID
        {
            get => ValidationHelper.GetValue<int?>(GetValue(nameof(TransformableViewCategoryParentID)), null);
            set => SetValue(nameof(TransformableViewCategoryParentID), value, null);
        }


        /// <summary>
        /// Transformable view category order.
        /// </summary>
        [DatabaseField]
        public virtual int TransformableViewCategoryOrder
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(TransformableViewCategoryOrder)), 0);
            set => SetValue(nameof(TransformableViewCategoryOrder), value);
        }


        /// <summary>
        /// Transformable view category last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime TransformableViewCategoryLastModified
        {
            get => ValidationHelper.GetDateTime(GetValue(nameof(TransformableViewCategoryLastModified)), DateTimeHelper.ZERO_TIME);
            set => SetValue(nameof(TransformableViewCategoryLastModified), value);
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            Provider.Delete(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            Provider.Set(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected TransformableViewCategoryInfo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="TransformableViewCategoryInfo"/> class.
        /// </summary>
        public TransformableViewCategoryInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="TransformableViewCategoryInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public TransformableViewCategoryInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}