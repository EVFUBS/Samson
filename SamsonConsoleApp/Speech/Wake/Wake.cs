using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Speech.Wake
{
    public class Wake
    {
        private readonly IAudioRecorder _audioRecorder;

        public Wake(IAudioRecorder audioRecorder)
        {
            _audioRecorder = audioRecorder;
        }

        public void WaitForWake()
        {
            _audioRecorder.StartRecording();
            while (true)
            {
                
            }
        }
    }
}
