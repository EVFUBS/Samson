using SamsonConsoleApp.Models.Samson;

namespace SamsonConsoleApp.Execute.Spotfiy.Interfaces
{
    public interface IExecuteSpotify
    {
        void Execute(SamsonAction action, string summary);
    }
}