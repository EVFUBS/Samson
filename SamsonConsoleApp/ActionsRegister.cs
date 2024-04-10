using SamsonConsoleApp.Execute;
using SamsonConsoleApp.Execute.DidNotUnderstand;
using SamsonConsoleApp.Execute.General;
using SamsonConsoleApp.Execute.Spotfiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp
{
    public class ActionsRegister(
        IActionCollection actionCollection,
        IExecuteGeneral general,
        IExecuteSpotify spotify,
        IExecuteDNU dnu) : IActionsRegister
    {
        public void RegisterActions()
        {
            actionCollection.RegisterAction(general);
            actionCollection.RegisterAction(spotify);
            actionCollection.RegisterAction(dnu);
        }
    }
}
