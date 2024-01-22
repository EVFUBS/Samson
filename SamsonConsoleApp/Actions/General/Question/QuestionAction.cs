using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Speech.GoogleTTS;

namespace SamsonConsoleApp.Actions.General.Question
{
    public class QuestionAction : IQuestionAction
    {
        private ISamsonAIClientFactory _samsonAiClientFactory;
        private ITextToSpeech _textToSpeech;

        public QuestionAction(
            ISamsonAIClientFactory samsonAIClientFactory,
            ITextToSpeech textToSpeech
            )
        {
            _samsonAiClientFactory = samsonAIClientFactory;
            _textToSpeech = textToSpeech;
        }

        public async Task Question(string summary)
        {
            var client = _samsonAiClientFactory.Create();
            var response = await client.GetSamsonQuestionAsync(new SamsonAIClient.SamsonQuestionRequest
            {
                Question = summary
            });
            await _textToSpeech.Say(response.Summary);
        }
    }
}
