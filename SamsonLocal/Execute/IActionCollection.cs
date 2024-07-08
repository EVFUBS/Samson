using SamsonCommon.Models;
using SamsonLocal.Execute.ExecuteActions;

namespace SamsonLocal.Execute
{
    public interface IActionCollection
    {
        void Execute(SamsonAction action);
        void RegisterAction(IExecuteAction action);
    }
}