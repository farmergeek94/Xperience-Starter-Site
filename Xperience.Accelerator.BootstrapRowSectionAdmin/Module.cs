using CMS;
using Kentico.Xperience.Admin.Base;

[assembly: RegisterModule(typeof(Xperience.Community.BootstrapRowSection.Module))]
namespace Xperience.Community.BootstrapRowSection
{
    public class Module : AdminModule
    {
        // (Optional) Change the name of the custom module
        public Module()
            : base("Xperience-Accelerator.Bootstrap-Row-Section")
        {
        }

        protected override void OnInit()
        {
            base.OnInit();

            // Change the organization name and project name in the client scripts registration
            RegisterClientModule("xperience-accelerator", "bootstrap-row-section");
        }
    }
}
