using SamsonCommon.Enums;
using SamsonCommon.Models;

namespace SamsonClient.Execute.ExecuteActions
{
    public interface IExecuteAction
    {
        Catergories catergory { get; }
        void Execute(SamsonAction action);
    }
}