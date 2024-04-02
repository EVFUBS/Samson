namespace SamsonConsoleApp.Execute.General.Question
{
    public interface IQuestionAction
    {
        Task Question(string summary);
    }
}