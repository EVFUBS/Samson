using SamsonConsoleApp.Models.Samson;

namespace SamsonConsoleApp.Execute.General
{
    public interface IExecuteGeneral
    {
        void Execute(SamsonAction action, string summary);
    }
}