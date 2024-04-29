using SamsonCommon.Models;
using SamsonConsoleApp.Execute.ExecuteActions;

namespace SamsonConsoleApp.Execute
{
    public class ActionCollection : IActionCollection
    {
        private readonly IList<IExecuteAction> actions = [];

        public void RegisterAction(IExecuteAction action)
        {
            actions.Add(action);
        }

        public void Execute(SamsonAction action)
        {
            foreach (var executeAction in actions)
            {
                if (executeAction.catergory == action.Catergory)
                {
                    executeAction.Execute(action);
                    break;
                }
            }
        }
    }
}
