namespace SamsonClient.Clients.Interfaces
{
    public interface IServerClientFactory
    {
        SamsonServerClient.SamsonServerClient Create();
    }
}