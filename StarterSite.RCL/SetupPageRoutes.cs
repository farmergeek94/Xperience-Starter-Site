using HBS.Xperience.TransformableViews.PageTemplates;
using Kentico.Content.Web.Mvc.Routing;
using X;

[assembly: RegisterWebPageRoute(
    contentTypeName: Page.CONTENT_TYPE_NAME,
    controllerType: typeof(TransformableViewController<Page>))]