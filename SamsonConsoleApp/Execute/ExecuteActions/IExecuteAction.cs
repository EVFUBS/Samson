using SamsonConsoleApp.Models.Samson;

namespace SamsonConsoleApp.Execute.ExecuteActions
{
    public interface IExecuteAction
    {
        void Execute(SamsonAction action, string summary);
    }
}