using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Speech.Wake
{
    public class Wake : IWake
    {
        private readonly IAudioRecorder _audioRecorder;

        public Wake(IAudioRecorder audioRecorder)
        {
            _audioRecorder = audioRecorder;
        }

        public void WaitForWake()
        {
            // find a way to wake samson up and put code for waiting for it here
            // will likely be something along the line of "Hey Samson" that will be listened for,
            // should be able to implement that myself
        }
    }
}
