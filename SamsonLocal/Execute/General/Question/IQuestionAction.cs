using SamsonCommon.Models;

namespace SamsonLocal.Execute.General.Question
{
    public interface IQuestionAction
    {
        Task Question(SamsonAction action);
    }
}