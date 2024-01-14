using SamsonAIClient;

namespace SamsonConsoleApp.Clients.Interfaces
{
    public interface ISamsonAIClientFactory
    {
        SamsonClient Create();
    }
}