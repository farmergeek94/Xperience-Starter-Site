using CMS;
using CMS.Helpers;
using Kentico.Xperience.Admin.Base;
using Microsoft.VisualBasic;

[assembly: RegisterModule(typeof(HBS.Xperience.Categories.CategoriesModule))]
namespace HBS.Xperience.Categories
{
    public class CategoriesModule : AdminModule
    {
        public CategoriesModule()
            : base("HBS.Xperience.Categories")
        {
        }

        protected override void OnInit()
        {
            base.OnInit();

            ContentItemCategoryInfo.TYPEINFO.Events.Insert.After += Update_After;

            ContentItemCategoryInfo.TYPEINFO.Events.Delete.After += Update_After;

            RegisterClientModule("hbs", "xperience-categories");
        }

        private void Update_After(object? sender, CMS.DataEngine.ObjectEventArgs e)
        {
            var cat = (ContentItemCategoryInfo)e.Object;
            CacheHelper.TouchKey($"{ContentItemCategoryInfo.OBJECT_TYPE}|categoryid|{cat.CategoryID}|contentitemid|{cat.ContentItemID}");
        }
    }
}
