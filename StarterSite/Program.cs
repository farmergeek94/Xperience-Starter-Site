using CMS.Core;
using CMS.Websites;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StarterSite.Logic;
using StarterSite.Logic.Context;
using StarterSite.RCL;
using System.Linq;
using X;
using Xperience.Accelerator.BootstrapRowSectionShared;
using Xperience.Community.ImageWidget;
using HBS.Xperience.TransformableViewsShared;
using HBS.Xperience.TransformableViews;
using CommandLine;
using XperienceComunity.TransformableViewsTool;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json", false, true)
     .Build();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    // Add Feature Folders
    options.ViewLocationFormats.AddFeatureFolders();
});

// Enable desired Kentico Xperience features
builder.Services.AddKentico(features =>
{
    features.UsePageBuilder(new PageBuilderOptions
    {
       ContentTypeNames = [
           Page.CONTENT_TYPE_NAME
       ]
    });
    // features.UseActivityTracking();
    features.UseWebPageRouting();
    // features.UseEmailStatisticsLogging();
    // features.UseEmailMarketing();
});

// Define the 
builder.Services.AddLogic();

builder.Services.AddScoped<ILayoutContext, LayoutContext>();

builder.Services.AddAuthentication();
// builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews().WithTransformableViews("QPiJvpcmTu6aUAMR7pgaNFI1DKHnWobx").UseTransformableViewsProvider();

builder.Services.AddBootstrapRowSection(x => x.SetupBackgroundItems(new string[] {
    "bg-primary"
    , "bg-secondary"
    , "bg-success"
    , "bg-danger"
    , "bg-warning"
    , "bg-info"
    , "bg-light"
    , "bg-dark"
    , "bg-body"
    , "bg-white"
    , "bg-transparent"
}));

builder.Services.AddImageWidget();

// Enables static web assets
builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

app.InitKentico();

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseAuthentication();


app.UseKentico();

// app.UseAuthorization();

app.Kentico().MapRoutes();
//app.MapGet("/", () => "The StarterSite site has not been configured yet.");

app.Run();