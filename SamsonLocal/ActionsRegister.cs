using SamsonLocal.Execute;
using SamsonLocal.Execute.DidNotUnderstand;
using SamsonLocal.Execute.General;
using SamsonLocal.Execute.Spotfiy;

namespace SamsonLocal
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
