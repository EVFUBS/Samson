using SamsonCommon.Enums;
using SamsonCommon.Models;

namespace SamsonLocal.Execute.ExecuteActions
{
    public interface IExecuteAction
    {
        Catergories catergory { get; }
        void Execute(SamsonAction action);
    }
}