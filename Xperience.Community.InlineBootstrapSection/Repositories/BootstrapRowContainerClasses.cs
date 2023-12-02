using Kentico.Xperience.Admin.Base.FormAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xperience.Community.BootstrapRowSection.Models;

namespace Xperience.Community.BootstrapRowSection.Repositories
{
    public class BootstrapRowContainerClasses : IDropDownOptionsProvider
    {
        private readonly IBootstrapRowOptions _rowOptions;

        public BootstrapRowContainerClasses(IBootstrapRowOptions rowOptions)
        {
            _rowOptions = rowOptions;
        }
        public Task<IEnumerable<DropDownOptionItem>> GetOptionItems()
        {
            return Task.FromResult(_rowOptions.BackgroundItems);
        }
    }
}