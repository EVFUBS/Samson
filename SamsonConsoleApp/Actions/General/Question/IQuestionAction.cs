namespace SamsonConsoleApp.Actions.General.Question
{
    public interface IQuestionAction
    {
        Task Question(string summary);
    }
}