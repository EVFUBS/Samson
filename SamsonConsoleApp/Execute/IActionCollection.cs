using SamsonConsoleApp.Execute.ExecuteActions;
using SamsonConsoleApp.Models.Samson;

namespace SamsonConsoleApp.Execute
{
    public interface IActionCollection
    {
        void Execute(SamsonAction action);
        void RegisterAction(IExecuteAction action);
    }
}