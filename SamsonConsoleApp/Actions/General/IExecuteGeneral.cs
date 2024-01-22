using SamsonAIClient;
using SamsonConsoleApp.enums;
using SamsonConsoleApp.Models.Samson;

namespace SamsonConsoleApp.Actions.General
{
    public interface IExecuteGeneral
    {
        void Execute(SamsonAction action, string summary);
    }
}