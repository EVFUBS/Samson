using Actions = SamsonCommon.Enums.Actions;
using Catergories = SamsonCommon.Enums.Catergories;

namespace SamsonCommon.Models
{
    public class SamsonAction
    {
        public Actions Action { get; set; }
        public Catergories Catergory { get; set; }
        public ActionParameters? Parameters { get; set; }
        public string Text { get; set; }
    }
}
