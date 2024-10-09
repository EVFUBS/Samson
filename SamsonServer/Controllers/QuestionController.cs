using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SamsonServer.Helpers;
using SamsonServer.Models.Question;
using SamsonServer.Providers.Question;

namespace SamsonServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController(IQuestionProvider questionProvider) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Answer>> Get(string chatMessage)
    {
        var chatResponse = await questionProvider.GenerateAnswer(chatMessage);
        return Ok(new Answer(chatResponse));
    }

    [HttpPost("stream")]
    public async Task<ActionResult<Answer>> Post(string chatMessage)
    {
        async Task StreamData(Stream outputStream)
        {
            await questionProvider.GenerateAnswer(chatMessage, outputStream);
        }
        return new FileCallbackResult("text/plain", StreamData);
    }
}