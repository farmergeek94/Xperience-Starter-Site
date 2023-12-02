using CMS.ContentEngine;
using CMS.DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.Categories
{
    public static class ExtensionMethods
    {
        public static WhereParameters ContentItemCategories(this WhereParameters whereParameters, IEnumerable<int> categoryIds)
        {
            var query = new ObjectQuery(ContentItemCategoryInfo.OBJECT_TYPE)
                .Columns(nameof(IContentItemCategoryItem.ContentItemID))
                .WhereIn(nameof(IContentItemCategoryItem.CategoryID), categoryIds.ToArray());

            whereParameters.WhereIn(nameof(IContentItemCategoryItem.ContentItemID), query);
            return whereParameters;
        }
    }
}
