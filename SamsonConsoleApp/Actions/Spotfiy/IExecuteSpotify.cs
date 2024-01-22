using SamsonConsoleApp.Models.Samson;

namespace SamsonConsoleApp.Actions.Spotfiy
{
    public interface IExecuteSpotify
    {
        void Execute(SamsonAction action, string summary);
    }
}