using Kentico.Xperience.Admin.Base.FormAnnotations;

namespace Xperience.Community.BootstrapRowSection.Models
{
    public interface IBootstrapRowOptions
    {
        IEnumerable<DropDownOptionItem> BackgroundItems { get; set; }
    }
}