using SamsonConsoleApp.Execute;
using SamsonConsoleApp.Execute.DidNotUnderstand;
using SamsonConsoleApp.Execute.General;
using SamsonConsoleApp.Execute.Spotfiy;

namespace SamsonConsoleApp
{
    public class ActionsRegister(
        IActionCollection actionCollection,
        IExecuteGeneral general,
        IExecuteSpotify spotify,
        IExecuteDNU didNotUnderstand) : IActionsRegister
    {
        public void RegisterActions()
        {
            actionCollection.RegisterAction(general);
            actionCollection.RegisterAction(spotify);
            actionCollection.RegisterAction(didNotUnderstand);
        }
    }
}
