namespace SamsonLocal.Clients.Interfaces
{
    public interface IServerClientFactory
    {
        SamsonServerClient.SamsonServerClient Create();
    }
}