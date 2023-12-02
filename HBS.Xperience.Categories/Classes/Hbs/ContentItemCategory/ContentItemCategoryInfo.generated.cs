using System;
using System.Data;
using System.Runtime.Serialization;
using System.Collections.Generic;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using HBS.Xperience.Categories;

[assembly: RegisterObjectType(typeof(ContentItemCategoryInfo), ContentItemCategoryInfo.OBJECT_TYPE)]

namespace HBS.Xperience.Categories
{
    /// <summary>
    /// Data container class for <see cref="ContentItemCategoryInfo"/>.
    /// </summary>
    [Serializable]
    public partial class ContentItemCategoryInfo : AbstractInfo<ContentItemCategoryInfo, IContentItemCategoryInfoProvider>, IContentItemCategoryItem
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "hbs.contentitemcategory";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(ContentItemCategoryInfoProvider), OBJECT_TYPE, "hbs.contentItemCategory", "ContentItemCategoryID", null, null, null, null, null, null, null)
        {
            TouchCacheDependencies = true,
            DependsOn = new List<ObjectDependency>()
            {
                new ObjectDependency("CategoryID", "hbs.category", ObjectDependencyEnum.Required),
                new ObjectDependency("ContentItemID", "cms.contentitem", ObjectDependencyEnum.Required),
            },
        };


        /// <summary>
        /// Content item category ID.
        /// </summary>
        [DatabaseField]
        public virtual int ContentItemCategoryID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(ContentItemCategoryID)), 0);
            set => SetValue(nameof(ContentItemCategoryID), value);
        }


        /// <summary>
        /// Category ID.
        /// </summary>
        [DatabaseField]
        public virtual int CategoryID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(CategoryID)), 0);
            set => SetValue(nameof(CategoryID), value);
        }


        /// <summary>
        /// Content item ID.
        /// </summary>
        [DatabaseField]
        public virtual int ContentItemID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(ContentItemID)), 0);
            set => SetValue(nameof(ContentItemID), value);
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
        protected ContentItemCategoryInfo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="ContentItemCategoryInfo"/> class.
        /// </summary>
        public ContentItemCategoryInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="ContentItemCategoryInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public ContentItemCategoryInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}