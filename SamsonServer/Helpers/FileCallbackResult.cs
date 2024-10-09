using Microsoft.AspNetCore.Mvc;

namespace SamsonServer.Helpers;

public class FileCallbackResult(string contentType, Func<Stream, Task> callback) : FileResult(contentType)
{
    public override async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        response.ContentType = ContentType;

        await callback(response.Body);
    }
}