namespace SamsonConsoleApp.Clients.Interfaces
{
    public interface IServerClientFactory
    {
        SamsonServerClient.SamsonServerClient Create();
    }
}