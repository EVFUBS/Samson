namespace SamsonServer.Providers.Question;

public interface IQuestionProvider
{
    Task<string> GenerateAnswer(string chatMessage);
    Task GenerateAnswer(string chatMessage, Stream responseStream);
}