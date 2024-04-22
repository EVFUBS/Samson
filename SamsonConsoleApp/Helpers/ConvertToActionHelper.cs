using SamsonCommon.Enums;
using SamsonCommon.Models;
using System.Text.Json;

namespace SamsonConsoleApp.Helpers
{
    public static class ConvertToActionHelper
    {
        public static SamsonAction ToAction(this SamsonServerClient.SamsonAction value)
        {
            return new SamsonAction
            {
                Action = value.Action.ToEnum<Actions>(),
                Catergory = value.Catergory.ToEnum<Catergories>(),
                Parameters = value.Parameters.ToActionParameters()
            };
        }

        public static ActionParameters ToActionParameters(this SamsonServerClient.ActionParameters value)
        {
            var serialised = JsonSerializer.Serialize(value);
            return JsonSerializer.Deserialize<ActionParameters>(serialised);
        }
    }
}
