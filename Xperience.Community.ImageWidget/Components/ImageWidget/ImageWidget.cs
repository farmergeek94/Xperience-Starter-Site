using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Kentico.Forms.Web.Mvc;
using Xperience.Community.ImageWidget.Components.ImageWidget;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using CMS.MediaLibrary;
using Xperience.Community.ImageWidget.Context;
using Xperience.Community.ImageWidget.Repositories.Interfaces;
using Xperience.Community.ImageWidget.Models;
using AngleSharp.Css;

[assembly: RegisterWidget(ImageWidgetViewComponent.IDENTIFIER, typeof(ImageWidgetViewComponent), "Image", propertiesType: typeof(ImageWidgetProperties), Description = "Places an image on the page", IconClass = "icon-picture", AllowCache = true)]
namespace Xperience.Community.ImageWidget.Components.ImageWidget
{
    [ViewComponent]
    public class ImageWidgetViewComponent : ViewComponent
    {
        public ImageWidgetViewComponent (IMediaFileRepository mediaFileRepository, ICacheScope cacheScope)
        {
            _mediaFileRepository = mediaFileRepository;
            _cacheScope = cacheScope;
        }
        public const string IDENTIFIER = "X.ImageWidget";
        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly ICacheScope _cacheScope;

        public async Task<IViewComponentResult> InvokeAsync(ComponentViewModel<ImageWidgetProperties> widgetProperties)
        {
            var properties = widgetProperties.Properties;
            _cacheScope.Begin();
            MediaFileItem? assetToMediaFileInfo = (await _mediaFileRepository.GetMediaFiles(properties.AssetItems.Select(x=>x.Identifier))).FirstOrDefault();
            widgetProperties.CacheDependencies.CacheKeys = _cacheScope.End();

            var model = new ImageWidgetViewModel()
            {
                ImageGuid = assetToMediaFileInfo?.FileGUID,
                ImageUrl = assetToMediaFileInfo?.FileUrl ?? "",
                Alt = string.IsNullOrWhiteSpace(properties.Alt) ? assetToMediaFileInfo?.FileTitle ?? "" : properties.Alt,
                CssClass = properties.CssClass,
                Width = string.IsNullOrWhiteSpace(properties.Width) ? null : "",
                Height = string.IsNullOrWhiteSpace(properties.Height) ? null : "",
                ImageSelector = new ImageSelectorModel
                {
                    PropertyName = nameof(ImageWidgetProperties.AssetItems),
                    PropertyValue = properties.AssetItems
                }
            };


            return View("~/Components/ImageWidget/_ImageWidget.cshtml", model);
        }
    }

    public class ImageSelectorModel
    {
        public string PropertyName { get; set; } = "";
        public IEnumerable<AssetRelatedItem> PropertyValue { get; set; } = Enumerable.Empty<AssetRelatedItem>();
    }

    public record ImageWidgetProperties : IWidgetProperties
    {
        [AssetSelectorComponent(Label = "Media Item", MaximumAssets = 1, AllowedExtensions = "bmp;gif;ico;png;wmf;jpg;jpeg;tiff;tif;webp;svg")]
        public IEnumerable<AssetRelatedItem> AssetItems { get; set; } = Enumerable.Empty<AssetRelatedItem>();

        [TextInputComponent(Label = "Image Alt", Order = 3, ExplanationText = "Leave blank to pull the alt from the media library file.")]
        public string Alt { get; set; } = string.Empty;

        [TextInputComponent(Label = "CSS Class", Order = 4)]
        public string CssClass { get; set; } = string.Empty;

        [TextInputComponent(Label = "Width", Order = 5)]
        public string Width { get; set; }= string.Empty;

        [TextInputComponent(Label = "Height", Order = 6)]
        public string Height { get; set; } = string.Empty;
    }

    public record ImageWidgetViewModel
    {
        public Guid? ImageGuid { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public string Alt { get; set; } = string.Empty;

        public string CssClass { get; set; } = string.Empty;

        public string? Width { get; set; }

        public string? Height { get; set; }

        public ImageSelectorModel? ImageSelector { get; set; } = null;
    }
   
}