using SamsonAIClient;
using SamsonConsoleApp.Enums;

namespace SamsonConsoleApp.Models.Samson
{
    public class SamsonAction
    {
        public Actions Action { get; set; }
        public Catergories Catergory { get; set; }
        public SamsonActionParameters? Parameters { get; set; }
    }
}
