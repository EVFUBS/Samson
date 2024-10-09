using SamsonCommon.Enums;
using SamsonCommon.Models;
using System.Text.Json;
using SamsonCommon.Helpers;

namespace SamsonClient.Helpers
{
    public static class ConvertToActionHelper
    {
        public static SamsonAction ToAction(this SamsonServerClient.SamsonAction value)
        {
            var action = (int)value.Action;
            var category = (int)value.Catergory;

            return new SamsonAction
            {
                Action = action.ToEnum<Actions>(),
                Catergory = category.ToEnum<Catergories>(),
                Parameters = value.Parameters.ToActionParameters()
            };
        }

        private static ActionParameters? ToActionParameters(this SamsonServerClient.ActionParameters value)
        {
            var serialised = JsonSerializer.Serialize(value);
            return JsonSerializer.Deserialize<ActionParameters>(serialised);
        }
    }
}
