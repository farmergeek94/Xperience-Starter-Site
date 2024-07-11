using CMS.ContentEngine;
using CMS.ContentEngine.Internal;
using CMS.Core;
using CMS.DataEngine;
using CMS.Helpers;
using HBS.TransformableViews_Experience;
using HBS.Xperience.TransformableViews.Components;
using HBS.Xperience.TransformableViews.Repositories;
using HBS.Xperience.TransformableViewsShared.Library;
using HBS.Xperience.TransformableViewsShared.Models;
using HBS.Xperience.TransformableViewsShared.Repositories;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using Kentico.Xperience.Admin.Base.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: RegisterWidget(
    identifier: TransformableViewContentWidgetViewComponent.Identifier,
    customViewName: "~/Components/_TransformableViewContentWidget.cshtml",
    name: "Transformable View Widget",
    propertiesType: typeof(TransformableViewContentWidgetProperties),
    IconClass = "icon-braces-octothorpe")]

namespace HBS.Xperience.TransformableViews.Components
{
    public class TransformableViewContentWidgetViewComponent : ViewComponent
    {
        public const string Identifier = "HBS.TransformableViewWidgets";

        private readonly ITransformableViewRepository _transformableViewRepository;
        private readonly IContentQueryExecutor _contentQueryExecutor;
        private readonly WebPageRetriever _webPageRetriever;

        public TransformableViewContentWidgetViewComponent(ITransformableViewRepository transformableViewRepository, IContentQueryExecutor contentQueryExecutor, WebPageRetriever webPageRetriever)
        {
            _transformableViewRepository = transformableViewRepository;
            _contentQueryExecutor = contentQueryExecutor;
            _webPageRetriever = webPageRetriever;
        }
        public async Task<IViewComponentResult> InvokeAsync(TransformableViewContentWidgetProperties properties)
        {
            if (properties.ContentType.Any())
            {
                var columNames = await _webPageRetriever.GetClassColumnNames(properties.ContentType.First().ObjectCodeName);

                // Builds the query - the content type must match the one configured for the selector
                var query = new ContentItemQueryBuilder()
                                .ForContentType(properties.ContentType.First().ObjectCodeName,
                                      config => config
                                        .Where(where =>
                                        where
                                                .WhereIn(nameof(IContentQueryDataContainer.ContentItemGUID), properties.SelectedContent.Select(x => x.Identifier).ToList())
                                        ));

                var items = await _contentQueryExecutor.GetResult(query, map =>
                {
                    var eOb = new ExpandoObject() as IDictionary<string, object?>;
                    eOb.Add(nameof(map.ContentItemID), map.ContentItemID);
                    eOb.Add(nameof(map.ContentItemContentTypeID), map.ContentItemContentTypeID);
                    eOb.Add(nameof(map.ContentItemGUID), map.ContentItemGUID);
                    eOb.Add(nameof(map.ContentItemCommonDataContentLanguageID), map.ContentItemCommonDataContentLanguageID);
                    eOb.Add(nameof(map.ContentItemName), map.ContentItemName);
                    foreach (var columnName in columNames)
                    {
                        if (eOb.ContainsKey(columnName))
                        {
                            continue;
                        }
                        if(map.TryGetValue(columnName, out dynamic value))
                        {
                            eOb.Add(columnName, value);
                        }
                    }
                    return (ExpandoObject)eOb; 
                });

                var viewModel = new TransformableViewModel()
                {
                    ViewTitle = properties.ViewTitle,
                    ViewClassNames = properties.ViewClassNames,
                    ViewCustomContent = properties.ViewCustomContent,
                    Items = items
                };
                return View(properties.View.FirstOrDefault()?.ObjectCodeName, viewModel);
            }
            return Content(string.Empty);
        }
    }

    public class TransformableViewContentWidgetProperties : IWidgetProperties
    {
        [ObjectSelectorComponent(DataClassInfo.OBJECT_TYPE, WhereConditionProviderType = typeof(TransformableViewContentTypeWhere), OrderBy = ["ClassDisplayName"], Label = "Content Type")]
        public IEnumerable<ObjectRelatedItem> ContentType { get; set; } = [];

        [ContentItemSelectorComponent(typeof(TransformableViewContentTypeFilter), Label = "Selected Content Items", Order = 1)]
        [VisibleIfNotEmpty(nameof(ContentType))]
        public IEnumerable<ContentItemReference> SelectedContent { get; set; } = [];

        [TextInputComponent(Label = "View Title")]
        public string ViewTitle { get; set; } = string.Empty;


        [TextInputComponent(Label = "View CSS Classes")]
        public string ViewClassNames { get; set; } = string.Empty;

        [TextAreaComponent(Label = "View Custom Content")]
        public string ViewCustomContent { get; set; } = string.Empty;

        [ObjectSelectorComponent(TransformableViewInfo.OBJECT_TYPE, WhereConditionProviderType = typeof(TransformableViewWhere), OrderBy = ["TransformableViewDisplayName"], Label = "View")]
        public IEnumerable<ObjectRelatedItem> View { get; set; } = [];
    }

    public class TransformableViewContentTypeFilter : IContentTypesFilter
    {
        public IEnumerable<Guid> AllowedContentTypeIdentifiers
        {
            get
            {
                var formFieldValueProvider = Service.ResolveOptional<IFormFieldValueProvider>();
                if(formFieldValueProvider == null)
                {
                    return [];
                }
                formFieldValueProvider.TryGet(nameof(TransformableViewContentWidgetProperties.ContentType), out string contentTypeName);
                var provider = DataClassInfoProvider.ProviderObject;
                var contentItem = provider.Get().Columns(nameof(DataClassInfo.ClassGUID)).Select(x=>x.ClassGUID);
                return contentItem;
            }
        }
    }

    public class TransformableViewContentTypeWhere : IObjectSelectorWhereConditionProvider
    {
        // Where condition limiting the objects
        public WhereCondition Get() => new WhereCondition().WhereEquals(nameof(DataClassInfo.ClassType), "Content");
    }
    public class TransformableViewWhere : IObjectSelectorWhereConditionProvider
    {
        // Where condition limiting the objects
        public WhereCondition Get() => new WhereCondition().WhereEquals(nameof(TransformableViewInfo.TransformableViewType), (int)TransformableViewTypeEnum.Transformable);
    }
}
