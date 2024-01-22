using SamsonConsoleApp.enums;
using SamsonConsoleApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Models.Samson
{
    public class SamsonAction
    {
        public SamsonActions Action { get; set; }
        public SamsonCatergories Catergories { get; set; }
        public IEnumerable<string> Parameters { get; set; }
    }
}
