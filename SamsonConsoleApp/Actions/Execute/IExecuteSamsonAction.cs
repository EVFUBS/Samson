using SamsonAIClient;
using SamsonConsoleApp.enums;

namespace SamsonConsoleApp.Actions.Execute
{
    public interface IExecuteSamsonAction
    {
        void Execute(SamsonActionResponse response);
    }
}