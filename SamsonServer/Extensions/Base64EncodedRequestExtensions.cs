using SamsonServer.Controllers;

namespace SamsonServer.Extensions;

public static class Base64EncodedRequestExtensions
{
    public static MemoryStream ToMemoryStream(this Base64EncodedRequest request)
    {
        var fileData = Convert.FromBase64String(request.FileData);
        return new MemoryStream(fileData);
    }
}