using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Actions
{
    public class Greet
    {
        public static void Greeting()
        {

            try
            {
                var synthesizer = new SpeechSynthesizer();
                synthesizer.SetOutputToDefaultAudioDevice();
                synthesizer.Speak("Hello, these are my first words, nice to meet you, I'm Samson");
            }
            catch
            {
                throw new Exception("Error not using windows");
            }
        }
    }
}
