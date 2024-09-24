using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xperience.Accelerator.BootstrapRowSectionShared.Models
{
    public class BootstrapColumnModel
    {
        private Guid? _id = null;
        // Use an ID to be able to keep the widgets that are in the current column when another column is removed. 
        public Guid Id { get { if (_id == null) { _id = Guid.NewGuid(); } return _id.Value; } set => _id = value; }
        public int Size { get; set; } = 12;
        public string CustomClass { get; set; } = "";
        public string GutterY { get; set; } = "";
        public string GutterX { get; set; } = "";
    }
}
