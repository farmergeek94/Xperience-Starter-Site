using Kentico.Web.Mvc;
using Kentico.Xperience.Cloud;
using Microsoft.Extensions.Hosting;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Microsoft.AspNetCore.Hosting;
using StarterSite.Logic;
using X;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsQa() || builder.Environment.IsUat() || builder.Environment.IsProduction())
{
    builder.Services.AddXperienceCloudSendGrid(builder.Configuration);
}

// Enable desired Kentico Xperience features
builder.Services.AddKentico(features =>
{
    features.UsePageBuilder(new PageBuilderOptions
    {
       ContentTypeNames = new[] { 
           Page.CONTENT_TYPE_NAME 
       }
    });
    // features.UseActivityTracking();
    features.UseWebPageRouting();
    // features.UseEmailStatisticsLogging();
    // features.UseEmailMarketing();
});

builder.Services.AddLogic();

builder.Services.AddAuthentication();
// builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

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
app.MapGet("/", () => "The StarterSite site has not been configured yet.");

app.Run();