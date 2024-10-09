using SamsonCommon.Models;

namespace SamsonClient.Execute.General.Question
{
    public interface IQuestionAction
    {
        Task Question(SamsonAction action);
    }
}