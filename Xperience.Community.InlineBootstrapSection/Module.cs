using CMS;
using Kentico.Xperience.Admin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xperience.Community.InlineBootstrapSection;

[assembly: RegisterModule(typeof(Module))]
namespace Xperience.Community.InlineBootstrapSection
{
    public class Module : AdminModule
    {
        // (Optional) Change the name of the custom module
        public Module()
            : base("Xperience-Community.Bootstrap-Row-Section")
        {
        }

        protected override void OnInit()
        {
            base.OnInit();

            // Change the organization name and project name in the client scripts registration
            RegisterClientModule("xperience-community", "bootstrap-row-section");
        }
    }
}
