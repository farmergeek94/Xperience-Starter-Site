using CMS.DataEngine;
using Kentico.Xperience.Admin.Websites;
using Kentico.Xperience.Admin.Websites.UIPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Websites.Internal;
using CMS.ContentEngine.Internal;
using CMS.ContentEngine;
using CMS.Helpers;
using HBS.Xperience.Categories.Admin.UIPages;

[assembly: UIPage(typeof(ContentItemEditSection), "hbs-category-select", typeof(CategorySelectPageContentItem), "Categories", CategorySelectPageContentItem.TemplateName, 150)]
[assembly: UIPage(typeof(WebPageLayout), "hbs-category-select-web", typeof(CategorySelectPageWebPage), "Categories", CategorySelectPageWebPage.TemplateName, 350, Icons.OrganisationalScheme)]

namespace HBS.Xperience.Categories.Admin.UIPages
{
    public class CategorySelectPageContentItem : Page<CategorySelectPageTemplateClientProperties>
    {
        private readonly ICategoryInfoProvider _categoryInfoProvider;
        private readonly IContentItemCategoryInfoProvider _contentItemCategoryInfoProvider;
        private readonly IInfoProvider<WebPageItemInfo> _webPageItemInfoProvider;
        private readonly IInfoProvider<ContentItemInfo> _contentItemInfoProvider;

        public CategorySelectPageContentItem(ICategoryInfoProvider categoryInfoProvider, IContentItemCategoryInfoProvider contentItemCategoryInfoProvider, IInfoProvider<WebPageItemInfo> webPageItemInfoProvider, IInfoProvider<ContentItemInfo> contentItemInfoProvider)
        {
            _categoryInfoProvider = categoryInfoProvider;
            _contentItemCategoryInfoProvider = contentItemCategoryInfoProvider;
            _webPageItemInfoProvider = webPageItemInfoProvider;
            _contentItemInfoProvider = contentItemInfoProvider;
        }

        public const string TemplateName = "@hbs/xperience-categories/CategorySelectContentItem";

        [PageParameter(typeof(IntPageModelBinder), typeof(ContentItemEditSection))]
        public int ContentItemID { get; set; }

        public override async Task<CategorySelectPageTemplateClientProperties> ConfigureTemplateProperties(CategorySelectPageTemplateClientProperties properties)
        {
            properties.Categories = await _categoryInfoProvider.Get().GetEnumerableTypedResultAsync();
            properties.ContentItemID = ContentItemID;
            properties.ContentItemCategories = await _contentItemCategoryInfoProvider.Get().Where(w => w.WhereEquals(nameof(IContentQueryDataContainer.ContentItemID), ContentItemID)).GetEnumerableTypedResultAsync();
            return properties;
        }


        [PageCommand]
        public async Task<ICommandResponse> AddContentItemCategory(int categoryID)
        {
            var contentItemCategory = new ContentItemCategoryInfo
            {
                ContentItemID = ContentItemID,
                CategoryID = categoryID
            };
            _contentItemCategoryInfoProvider.Set(contentItemCategory);
            return ResponseFrom(categoryID);
        }

        [PageCommand]
        public async Task<ICommandResponse> RemoveContentItemCategory(int categoryID)
        {
            var contentItemCategories = await _contentItemCategoryInfoProvider.Get().Where(x=>x.WhereEquals(nameof(ContentItemCategoryInfo.ContentItemID), ContentItemID).WhereEquals(nameof(ContentItemCategoryInfo.CategoryID), categoryID)).GetEnumerableTypedResultAsync();
            foreach(var item in contentItemCategories){
                _contentItemCategoryInfoProvider.Delete(item);
            }
            return ResponseFrom(categoryID);
        }
    }

    public class CategorySelectPageWebPage : Page<CategorySelectPageTemplateClientProperties>
    {
        private readonly ICategoryInfoProvider _categoryInfoProvider;
        private readonly IContentItemCategoryInfoProvider _contentItemCategoryInfoProvider;
        private readonly IInfoProvider<WebPageItemInfo> _webPageItemInfoProvider;
        private readonly IInfoProvider<ContentItemInfo> _contentItemInfoProvider;

        public CategorySelectPageWebPage(ICategoryInfoProvider categoryInfoProvider, IContentItemCategoryInfoProvider contentItemCategoryInfoProvider, IInfoProvider<WebPageItemInfo> webPageItemInfoProvider, IInfoProvider<ContentItemInfo> contentItemInfoProvider)
        {
            _categoryInfoProvider = categoryInfoProvider;
            _contentItemCategoryInfoProvider = contentItemCategoryInfoProvider;
            _webPageItemInfoProvider = webPageItemInfoProvider;
            _contentItemInfoProvider = contentItemInfoProvider;
        }

        public const string TemplateName = "@hbs/xperience-categories/CategorySelectPage";

        private int? _contentItemID = null;

        public int ContentItemID { 
            get { 
                _contentItemID ??= FinalContentItemID();
                return _contentItemID.Value;
            } 
        }

        [PageParameter(typeof(WebPageUrlIdentifierPageModelBinder), typeof(WebPageLayout))]
        public WebPageUrlIdentifier WebPageIdentifier { get; set; }

        private int FinalContentItemID()
        {
            int contentItemID = -1;
            // Use the generic info providers to get the content item id.
            var page = _webPageItemInfoProvider.Get(WebPageIdentifier.WebPageItemID);
            var contentItem = _contentItemInfoProvider.Get(page.WebPageItemContentItemID);

            //
            if (contentItem.ContentItemContentTypeID >= 0)
            {
                contentItemID = page.WebPageItemContentItemID;
            }
            return contentItemID;
        }

        public override async Task<CategorySelectPageTemplateClientProperties> ConfigureTemplateProperties(CategorySelectPageTemplateClientProperties properties)
        {
            properties.Categories = await _categoryInfoProvider.Get().GetEnumerableTypedResultAsync();
            properties.ContentItemID = ContentItemID;
            properties.ContentItemCategories = await _contentItemCategoryInfoProvider.Get().Where(w => w.WhereEquals(nameof(IContentQueryDataContainer.ContentItemID), ContentItemID)).GetEnumerableTypedResultAsync();
            return properties;
        }

        [PageCommand]
        public async Task<ICommandResponse> AddContentItemCategory(int categoryID)
        {
            var contentItemCategory = new ContentItemCategoryInfo
            {
                ContentItemID = ContentItemID,
                CategoryID = categoryID
            };
            _contentItemCategoryInfoProvider.Set(contentItemCategory);
            return ResponseFrom(categoryID);
        }

        [PageCommand]
        public async Task<ICommandResponse> RemoveContentItemCategory(int categoryID)
        {
            var contentItemCategories = await _contentItemCategoryInfoProvider.Get().Where(x => x.WhereEquals(nameof(ContentItemCategoryInfo.ContentItemID), ContentItemID).And().WhereEquals(nameof(ContentItemCategoryInfo.CategoryID), categoryID)).GetEnumerableTypedResultAsync();
            foreach (var item in contentItemCategories)
            {
                _contentItemCategoryInfoProvider.Delete(item);
            }
            return ResponseFrom(categoryID);
        }
    }


    // Contains properties that match the properties of the client template
    // Specify such classes as the generic parameter of Page<TClientProperties>
    public class CategorySelectPageTemplateClientProperties : TemplateClientProperties
    {
        // For example
        public IEnumerable<ICategoryItem> Categories { get; set; } = Enumerable.Empty<ICategoryItem>();

        public IEnumerable<IContentItemCategoryItem> ContentItemCategories { get; set; } = Enumerable.Empty<IContentItemCategoryItem>();

        public int? ContentItemID { get; set; }
    }
}
