using SamsonAIClient;

namespace SamsonConsoleApp.Clients.Interfaces
{
    public interface IAiClientFactory
    {
        SamsonClient Create();
    }
}