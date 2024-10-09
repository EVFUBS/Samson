using SamsonClient.Execute;
using SamsonClient.Execute.DidNotUnderstand;
using SamsonClient.Execute.General;
using SamsonClient.Execute.Spotfiy;

namespace SamsonClient
{
    public class ActionsRegister(
        IActionCollection actionCollection,
        IExecuteGeneral general,
        IExecuteSpotify spotify,
        IExecuteDnu didNotUnderstand) : IActionsRegister
    {
        public void RegisterActions()
        {
            actionCollection.RegisterAction(general);
            actionCollection.RegisterAction(spotify);
            actionCollection.RegisterAction(didNotUnderstand);
        }
    }
}
