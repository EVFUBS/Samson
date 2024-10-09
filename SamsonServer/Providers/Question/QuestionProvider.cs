using System.Text;
using OllamaSharp;

namespace SamsonServer.Providers.Question;

public class QuestionProvider : IQuestionProvider
{
    public async Task<string> GenerateAnswer(string chatMessage)
    {
        var uri = new Uri("http://localhost:11434");
        var ollama = new OllamaApiClient(uri)
        {
            SelectedModel = "llama3.1",
        };

        var chat = new Chat(ollama, "Your name is Samson you are a AI chatbot and will answer any question to the best of your ability. Please format your answers to text.");
        var asyncChatResponse = chat.Send(chatMessage);

        var chatResponse = "";
        await foreach (var answerToken in asyncChatResponse)
        {
            chatResponse += answerToken;
        }

        return chatResponse;
    }
    
    public async Task GenerateAnswer(string chatMessage, Stream responseStream)
    {
        var uri = new Uri("http://localhost:11434");
        var ollama = new OllamaApiClient(uri)
        {
            SelectedModel = "llama3.1",
        };

        var chat = new Chat(ollama, "Your name is Samson you are a AI chatbot and will answer any question to the best of your ability. Please format your answers to text.");
        var asyncChatResponse = chat.Send(chatMessage);

        await using var writer = new StreamWriter(responseStream, new UTF8Encoding(false), 1024, leaveOpen: true);
        await foreach (var answerToken in asyncChatResponse)
        {
            await writer.WriteAsync(answerToken);
            await writer.FlushAsync();
        }
    }
}