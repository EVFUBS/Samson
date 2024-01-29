using SamsonConsoleApp.Models.Samson;

namespace SamsonConsoleApp.Actions.Spotfiy.Interfaces
{
    public interface IExecuteSpotify
    {
        void Execute(SamsonAction action, string summary);
    }
}