using SamsonCommon.Models;
using SamsonLocal.Execute.ExecuteActions;

namespace SamsonLocal.Execute
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
                if (executeAction.catergory == action.Catergory)
                {
                    executeAction.Execute(action);
                    break;
                }
            }
        }
    }
}
