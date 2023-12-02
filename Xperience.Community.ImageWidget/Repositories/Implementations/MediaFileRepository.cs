using CMS.Helpers;
using CMS.MediaLibrary;
using CMS.Websites.Routing;
using Kentico.Content.Web.Mvc;
using Xperience.Community.ImageWidget.Context;
using Xperience.Community.ImageWidget.Library;
using Xperience.Community.ImageWidget.Models;
using Xperience.Community.ImageWidget.Repositories.Interfaces;

namespace Xperience.Community.ImageWidget.Repositories.Implementations
{
    public class MediaFileRepository : IMediaFileRepository
    {
        private readonly IMediaFileUrlRetriever _mediaFileUrlRetriever;
        private readonly IMediaFileInfoProvider _mediaFileInfoProvider;
        private readonly IProgressiveCache _progressiveCache;
        private readonly IWebsiteChannelContext _websiteChannelContext;
        private readonly ICacheScope _cacheScope;

        public MediaFileRepository(IMediaFileUrlRetriever mediaFileUrlRetriever, IMediaFileInfoProvider mediaFileInfoProvider, IProgressiveCache progressiveCache, IWebsiteChannelContext websiteChannelContext, ICacheScope cacheScope)
        {
            _mediaFileUrlRetriever = mediaFileUrlRetriever;
            _mediaFileInfoProvider = mediaFileInfoProvider;
            _progressiveCache = progressiveCache;
            _websiteChannelContext = websiteChannelContext;
            _cacheScope = cacheScope;
        }
        public async Task<IEnumerable<MediaFileItem>> GetMediaFiles(IEnumerable<Guid> mediaGuids)
        {
            var cacheKeys = new CacheKeyHelper(_websiteChannelContext, _cacheScope);
            foreach(var guid in mediaGuids)
            {
                cacheKeys.Media(guid);
            }
            return await _progressiveCache.LoadAsync(async cs =>
            {
                if (cs.Cached)
                {
                    cs.CacheDependency = cacheKeys.GetCMSCacheDependency();
                } 
                return (await _mediaFileInfoProvider.Get()
                .Where(w =>
                {
                    if (mediaGuids != null && mediaGuids.Any())
                    {
                        w.WhereIn(nameof(MediaFileInfo.FileGUID), mediaGuids.ToArray());
                    }
                    else
                    {
                        w.NoResults();
                    }
                }).Columns("FileExtension", "FileLibraryID", "FileName", "FileGUID", "FilePath", "FileTitle").GetEnumerableTypedResultAsync()).Select(x =>
            {
                var urlItem = _mediaFileUrlRetriever.Retrieve(x);
                return new MediaFileItem
                {
                    FileExtension = x.FileExtension,
                    FileTitle = x.FileTitle,
                    FileName = x.FileName,
                    FileGUID = x.FileGUID,
                    FilePath = x.FilePath,
                    FileUrl = urlItem.RelativePath,
                    FileDirectUrl = urlItem.DirectPath,
                };
            });
            }, new CacheSettings(36000, "GetMediaFiles", string.Join("|", mediaGuids)));
        }
    }
}
