using CMS.Base;
using CMS.ContentEngine;
using CMS.DataEngine;
using CMS.FormEngine;
using CMS.FormEngine.Internal;
using CMS.Helpers;
using CMS.Websites;
using CMS.Websites.Routing;
using Kentico.Content.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViews.Repositories
{
    public class ContentItemRetriever
    {
        private readonly IWebPageDataContextRetriever _contextRetriever;
        private readonly IContentQueryExecutor _queryExecutor;
        private readonly IWebsiteChannelContext _channelContext;

        public ContentItemRetriever(IWebPageDataContextRetriever contextRetriever,
            IContentQueryExecutor queryExecutor, IWebsiteChannelContext channelContext)
        {
            _contextRetriever = contextRetriever;
            _queryExecutor = queryExecutor;
            _channelContext = channelContext;
        }

        internal async Task<IEnumerable<string>> GetClassColumnNames(string className)
        {
            var type = await DataClassInfoProvider.ProviderObject.GetAsync(className);
            return await GetClassColumnsNames(type);
        }

        internal async Task<IEnumerable<string>> GetClassColumnsNames(DataClassInfo? type)
        {
            var form = new FormInfo(type.ClassFormDefinition);
            return form.GetColumnNames();
        }

        internal async Task<dynamic?> GetWebPage(bool isAuthenticated)
        {
            var page = _contextRetriever.Retrieve().WebPage;

            var columnNames = await GetClassColumnNames(page.ContentTypeName);

            var builder = new ContentItemQueryBuilder()
                .ForContentType(page.ContentTypeName,
                options => options
                .ForWebsite(page.WebsiteChannelName)
                .Where(x => x.WhereEquals(nameof(IWebPageContentQueryDataContainer.WebPageItemID), page.WebPageItemID))
                .TopN(1)
                ).InLanguage(page.LanguageName);

            // Configures the query options for the query executor
            var queryOptions = new ContentQueryExecutionOptions()
            {
                ForPreview = _channelContext.IsPreview,
                IncludeSecuredItems = isAuthenticated || _channelContext.IsPreview
            };

            var result = await _queryExecutor.GetResult(builder, map => GetColumnValues(map, columnNames), queryOptions);

            return (result).FirstOrDefault();
        }

        internal async Task<ExpandoObject[]> GetContentItems(Guid? contentType, IEnumerable<Guid> selectedContent)
        {
            // Retrive the DataClass in order to get the FormDefinition.  Have to query it from the table because content types are not loaded from the database. 
            var type = (await DataClassInfoProvider.ProviderObject.Get().WhereEquals(nameof(DataClassInfo.ClassGUID), contentType).GetEnumerableTypedResultAsync()).FirstOrDefault();

            // Parse out the column names from the form definition
            var columNames = await GetClassColumnsNames(type);

            // Builds the query - the content type must match the one configured for the selector
            var query = new ContentItemQueryBuilder()
                            .ForContentType(type.ClassName,
                                  config => config
                                    .Where(where =>
                                    where
                                            .WhereIn(nameof(IContentQueryDataContainer.ContentItemGUID), selectedContent.ToList())
                                    ));

            // builds the expando object columns
            ExpandoObject[] items = (await _queryExecutor.GetResult(query, map => GetColumnValues(map, columNames))).ToArray();
            return items;
        }

        internal ExpandoObject GetColumnValues(IContentQueryDataContainer map, IEnumerable<string> columnNames)
        {
            var eOb = new ExpandoObject() as IDictionary<string, object?>;
            eOb.Add(nameof(map.ContentItemID), map.ContentItemID);
            eOb.Add(nameof(map.ContentItemContentTypeID), map.ContentItemContentTypeID);
            eOb.Add(nameof(map.ContentItemGUID), map.ContentItemGUID);
            eOb.Add(nameof(map.ContentItemCommonDataContentLanguageID), map.ContentItemCommonDataContentLanguageID);
            eOb.Add(nameof(map.ContentItemName), map.ContentItemName);
            eOb.Add(nameof(map.ContentTypeName), map.ContentTypeName);
            foreach (var columnName in columnNames)
            {
                if (eOb.ContainsKey(columnName))
                {
                    continue;
                }
                if (map.TryGetValue(columnName, out dynamic value))
                {
                    if (value.GetType() == typeof(string) && (value.IndexOf("[") > -1 || value.IndexOf("{") > -1))
                    {
                        try
                        {
                            var parsed = JsonSerializer.Deserialize<ExpandoObject>(value);
                            eOb.Add(columnName, parsed);
                            continue;
                        }
                        catch
                        {

                        }
                    }
                    eOb.Add(columnName, value);
                }
            }
            return (ExpandoObject)eOb;
        }
    }
}
