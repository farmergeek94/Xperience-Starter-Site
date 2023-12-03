using CMS.Core;
using CMS;
using CMS.DataEngine;
using HBS.Xperience.TransformableViews.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kentico.Xperience.Admin.Base;
using HBS.Xperience.TransformableViews;

[assembly: RegisterModule(typeof(TransformableViewModule))]

namespace HBS.Xperience.TransformableViews
{
    internal class TransformableViewModule : AdminModule
    {
        public TransformableViewModule() : base("HBS.Xperience.TransformableViews")
        {
        }

        protected override void OnInit()
        {
            base.OnInit();
            RegisterClientModule("hbs", "xperience-transformable-views");
        }
    }
}
