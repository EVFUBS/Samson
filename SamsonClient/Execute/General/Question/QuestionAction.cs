using SamsonClient.Clients.Interfaces;
using SamsonCommon.Models;

namespace SamsonClient.Execute.General.Question
{
    public class QuestionAction(IServerClientFactory samsonServerClientFactory) : IQuestionAction
    {
        public async Task Question(SamsonAction action)
        {
            var client = samsonServerClientFactory.Create();

            var response = await client.QuestionAsync(action.Text);
            
            // TODO: change this to audio when we go that working
            Console.WriteLine(response.Text);
        }
    }
}
