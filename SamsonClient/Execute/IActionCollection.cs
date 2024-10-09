using SamsonClient.Execute.ExecuteActions;
using SamsonCommon.Models;

namespace SamsonClient.Execute
{
    public interface IActionCollection
    {
        void Execute(SamsonAction action);
        void RegisterAction(IExecuteAction action);
    }
}