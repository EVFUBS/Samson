using SamsonConsoleApp.enums;
using SamsonConsoleApp.Enums;
using SamsonConsoleApp.Models.Samson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Helpers
{
    public static class ConvertToActionHelper
    {
        public static SamsonAction ToAction(this SamsonAIClient.SamsonActionResponse value)
        {
            return new SamsonAction
            {
                Action = value.Action.ToEnum<SamsonActions>(),
                Catergories = value.Catergory.ToEnum<SamsonCatergories>(),
                Parameters = value.Parameters
            };
        }
    }
}
