using CMS.MediaLibrary;
using Xperience.Community.ImageWidget.Models;

namespace Xperience.Community.ImageWidget.Repositories.Interfaces
{
    public interface IMediaFileRepository
    {
        Task<IEnumerable<MediaFileItem>> GetMediaFiles(IEnumerable<Guid> assets);
    }
}