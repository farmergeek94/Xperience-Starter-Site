using System;
using System.Data;
using System.Runtime.Serialization;
using System.Collections.Generic;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using HBS.Xperience.Categories;

[assembly: RegisterObjectType(typeof(CategoryInfo), CategoryInfo.OBJECT_TYPE)]

namespace HBS.Xperience.Categories
{
    /// <summary>
    /// Data container class for <see cref="CategoryInfo"/>.
    /// </summary>
    [Serializable, InfoCache(InfoCacheBy.ID | InfoCacheBy.Name | InfoCacheBy.Guid)]
    public partial class CategoryInfo : AbstractInfo<CategoryInfo, ICategoryInfoProvider>, ICategoryItem
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "hbs.category";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(CategoryInfoProvider), OBJECT_TYPE, "hbs.category", "CategoryID", "CategoryLastModified", "CategoryGuid", "CategoryName", "CategoryDisplayName", null, "CategoryParentID", "hbs.category")
        {
            TouchCacheDependencies = true,
            DependsOn = new List<ObjectDependency>()
            {
            },
        };


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
        /// Category guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid CategoryGuid
        {
            get => ValidationHelper.GetGuid(GetValue(nameof(CategoryGuid)), Guid.Empty);
            set => SetValue(nameof(CategoryGuid), value);
        }


        /// <summary>
        /// Category name.
        /// </summary>
        [DatabaseField]
        public virtual string CategoryName
        {
            get => ValidationHelper.GetString(GetValue(nameof(CategoryName)), String.Empty);
            set => SetValue(nameof(CategoryName), value);
        }


        /// <summary>
        /// Category display name.
        /// </summary>
        [DatabaseField]
        public virtual string CategoryDisplayName
        {
            get => ValidationHelper.GetString(GetValue(nameof(CategoryDisplayName)), String.Empty);
            set => SetValue(nameof(CategoryDisplayName), value);
        }


        /// <summary>
        /// Category parent ID.
        /// </summary>
        [DatabaseField]
        public virtual int? CategoryParentID
        {
            get {
                int? returnVal = null;
                if(int.TryParse(GetValue(nameof(CategoryParentID))?.ToString(), out int outval))
                {
                    returnVal = outval;
                }
                return returnVal;
            }
            set => SetValue(nameof(CategoryParentID), value, null);
        }

        /// <summary>
        /// Category ID.
        /// </summary>
        [DatabaseField]
        public virtual int CategoryOrder
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(CategoryOrder)), 0);
            set => SetValue(nameof(CategoryOrder), value);
        }


        /// <summary>
        /// Category last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime CategoryLastModified
        {
            get => ValidationHelper.GetDateTime(GetValue(nameof(CategoryLastModified)), DateTimeHelper.ZERO_TIME);
            set => SetValue(nameof(CategoryLastModified), value);
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
        protected CategoryInfo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="CategoryInfo"/> class.
        /// </summary>
        public CategoryInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="CategoryInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public CategoryInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}