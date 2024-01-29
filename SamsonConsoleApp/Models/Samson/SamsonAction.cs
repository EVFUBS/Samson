using SamsonAIClient;
using SamsonActions = SamsonConsoleApp.Enums.SamsonActions;
using SamsonCatergories = SamsonConsoleApp.Enums.SamsonCatergories;

namespace SamsonConsoleApp.Models.Samson
{
    public class SamsonAction
    {
        public SamsonActions Action { get; set; }
        public SamsonCatergories Catergories { get; set; }
        public SamsonActionParameters Parameters { get; set; }
    }
}
