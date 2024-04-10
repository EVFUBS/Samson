using SamsonConsoleApp.Models.Samson;

namespace SamsonConsoleApp.Execute.General.Question
{
    public interface IQuestionAction
    {
        Task Question(SamsonAction action);
    }
}