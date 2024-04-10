using SamsonConsoleApp.Enums;
using SamsonConsoleApp.Models.Samson;

namespace SamsonConsoleApp.Execute.ExecuteActions
{
    public interface IExecuteAction
    {
        Catergories catergory { get; }
        void Execute(SamsonAction action);
    }
}