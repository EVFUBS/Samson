using SamsonConsoleApp.Models.Samson;

namespace SamsonConsoleApp.Actions.Execute
{
    public interface IExecuteSamsonAction
    {
        void Execute(SamsonAction action, string summary);
    }
}