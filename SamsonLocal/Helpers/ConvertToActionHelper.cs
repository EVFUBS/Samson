using System.Text.Json;
using SamsonCommon.Helpers;
using ActionParameters = SamsonCommon.Models.ActionParameters;
using Actions = SamsonCommon.Enums.Actions;
using Catergories = SamsonCommon.Enums.Catergories;
using SamsonAction = SamsonCommon.Models.SamsonAction;

namespace SamsonLocal.Helpers
{
    public static class ConvertToActionHelper
    {
        public static SamsonAction ToAction(this SamsonServerClient.SamsonAction value)
        {
            var action = (int)value.Action;
            var catergory = (int)value.Catergory;

            return new SamsonAction
            {
                Action = action.ToEnum<Actions>(),
                Catergory = catergory.ToEnum<Catergories>(),
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
