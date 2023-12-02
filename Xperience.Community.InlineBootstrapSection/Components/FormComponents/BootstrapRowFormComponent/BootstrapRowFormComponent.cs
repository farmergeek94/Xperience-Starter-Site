﻿using CMS.FormEngine;
using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using Kentico.Xperience.Admin.Base.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xperience.Community.BootstrapRowSection.Models;

namespace Xperience.Community.BootstrapRowSection.Components.FormComponents.BootstrapRowFormComponent
{
    [ComponentAttribute(typeof(BootstrapRowFormComponentAttribute))]
    public class BootstrapRowFormComponent : FormComponent<BootstrapRowFormComponentProperties, BootstrapRowFormComponentClientProperties, IEnumerable<BootstrapColumnModel>>
    {
        public override string ClientComponentName => "@xperience-community/bootstrap-row-section/BootstrapRow";

        protected override Task ConfigureClientProperties(BootstrapRowFormComponentClientProperties clientProperties)
        {
            base.ConfigureClientProperties(clientProperties);

            if(!clientProperties.Value.Any())
            {
                clientProperties.Value = new List<BootstrapColumnModel> { new BootstrapColumnModel() };
            }

            return Task.CompletedTask;
        }
    }

    public class BootstrapRowFormComponentProperties : FormComponentProperties
    {
    }

    public class BootstrapRowFormComponentClientProperties : FormComponentClientProperties<IEnumerable<BootstrapColumnModel>>
    {
    }

    public class BootstrapRowFormComponentAttribute : FormComponentAttribute
    {
    }
}
