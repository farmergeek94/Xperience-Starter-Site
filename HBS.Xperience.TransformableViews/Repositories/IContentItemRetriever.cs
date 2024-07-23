﻿using HBS.Xperience.TransformableViewsShared.Models;
using System.Dynamic;

namespace HBS.Xperience.TransformableViews.Repositories
{
    public interface IContentItemRetriever
    {
        Task<ExpandoObject[]> GetContentItems(Guid? contentType, IEnumerable<Guid> selectedContent);
        Task<ISet<string>> GetDependencyCacheKeys(ExpandoObject[] items);
        Task<IEnumerable<dynamic>> GetObjectItems(TransformableViewObjectsFormComponentModel model);
        Task<dynamic?> GetWebPage(bool isAuthenticated);
    }
}