using SamsonCommon.Models;
using SamsonLocal.Speech.GoogleTTS;

namespace SamsonLocal.Execute.General.Question
{
    public class QuestionAction : IQuestionAction
    {
        private ITextToSpeech _textToSpeech;

        public QuestionAction(
            ITextToSpeech textToSpeech
            )
        {
            _textToSpeech = textToSpeech;
        }

        public async Task Question(SamsonAction action)
        {
            // come back and get question out of action later
            //var client = _samsonAiClientFactory.Create();
            //var response = await client.GetSamsonQuestionAsync(new QuestionRequest
            //{
            //    Question = "summary"
            //});
            //await _textToSpeech.Say(response.Summary);


            Console.WriteLine("Question");
        }
    }
}
