using SamsonAIClient;
using SamsonCommon.Models;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Speech.GoogleTTS;

namespace SamsonConsoleApp.Execute.General.Question
{
    public class QuestionAction : IQuestionAction
    {
        private IAiClientFactory _samsonAiClientFactory;
        private ITextToSpeech _textToSpeech;

        public QuestionAction(
            IAiClientFactory samsonAIClientFactory,
            ITextToSpeech textToSpeech
            )
        {
            _samsonAiClientFactory = samsonAIClientFactory;
            _textToSpeech = textToSpeech;
        }

        public async Task Question(SamsonAction action)
        {
            // come back and get question out of action later
            var client = _samsonAiClientFactory.Create();
            var response = await client.GetSamsonQuestionAsync(new QuestionRequest
            {
                Question = "summary"
            });
            await _textToSpeech.Say(response.Summary);
        }
    }
}
