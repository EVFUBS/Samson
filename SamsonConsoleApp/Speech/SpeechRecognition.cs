using NAudio.Wave;
using SamsonConsoleApp.Actions.Spotfiy.Interfaces;
using System.Runtime.InteropServices;
using System.Speech.Recognition;

namespace SamsonConsoleApp.Speech
{
    public class SpeechRecognition : ISpeechRecognition
    {
        private readonly ISpotifyAuthorisation _spotifyIntegration;
        private readonly ISpotifyPlayer _spotifyPlayer;

        public SpeechRecognition(ISpotifyAuthorisation spotifyIntegration, ISpotifyPlayer spotifyPlayer)
        {
            _spotifyIntegration = spotifyIntegration;
            _spotifyPlayer = spotifyPlayer;
        }

        public void RecogniseSpeech()
        {
            //_spotifyIntegration.Authorize();
            //_spotifyPlayer.PausePlayback();
            RecordAudio();
        }

        void RecognizerSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }

        void RecordAudio()
        {
            var outputFolder = Path.Combine(Path.GetTempPath(), "SamsonRecording");
            Directory.CreateDirectory(outputFolder);
            var outputFilePath = Path.Combine(outputFolder, "recorded.wav");

            var waveIn = new WaveInEvent();
            WaveFileWriter writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
            waveIn.StartRecording();

            waveIn.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
                if (writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 3)
                {
                    waveIn.StopRecording();
                }
            };

            waveIn.RecordingStopped += (s, a) =>
            {
                writer?.Dispose();
                waveIn.Dispose();
            };
        }
    }
}
