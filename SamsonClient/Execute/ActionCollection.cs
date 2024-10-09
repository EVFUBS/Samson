using SamsonClient.Execute.ExecuteActions;
using SamsonCommon.Models;

namespace SamsonClient.Execute
{
    public class ActionCollection : IActionCollection
    {
        private readonly IList<IExecuteAction> _actions = [];

        public void RegisterAction(IExecuteAction action)
        {
            _actions.Add(action);
        }

        public void Execute(SamsonAction action)
        {
            foreach (var executeAction in _actions)
            {
                if (executeAction.catergory != action.Catergory) continue;
                executeAction.Execute(action);
                break;
            }
        }
    }
}
