using StarterSite.Logic.Library;
using StarterSite.Models;

namespace StarterSite.Logic.Services.Interfaces
{
    public interface ICacheKeyService
    {
        CacheKeyHelper Create();
    }
}