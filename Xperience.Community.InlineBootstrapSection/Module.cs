using CMS;
using CMS.Core;
using CMS.DataEngine;
using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xperience.Community.BootstrapRowSection;

[assembly: RegisterModule(typeof(Xperience.Community.BootstrapRowSection.Module))]
namespace Xperience.Community.BootstrapRowSection
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
