using SamsonCommon.Models;
using SamsonConsoleApp.Execute.ExecuteActions;

namespace SamsonConsoleApp.Execute
{
    public interface IActionCollection
    {
        void Execute(SamsonAction action);
        void RegisterAction(IExecuteAction action);
    }
}