using NAudio.Wave;
using SamsonConsoleApp.Actions.Spotfiy.Interfaces;
using SamsonConsoleApp.Constants;
using SamsonConsoleApp.Helpers.AudioHelpers;
using SamsonConsoleApp.Speech.Deepgram;
using SamsonConsoleApp.Speech.Wake;
using System.Net.Sockets;
using System.Speech.Recognition;

namespace SamsonConsoleApp.Speech
{
    public class SpeechRecognition : ISpeechRecognition
    {
        private readonly ISpotifyAuthorisation _spotifyIntegration;
        private readonly ISpotifyPlayer _spotifyPlayer;
        private readonly ISpeechDeepgram _deepgram;
        private readonly IWake _wake;
        private readonly IAudioRecorder _audioRecorder;

        public SpeechRecognition(
            ISpotifyAuthorisation spotifyIntegration, 
            ISpotifyPlayer spotifyPlayer, 
            ISpeechDeepgram deepgram,
            IWake wake,
            IAudioRecorder audioRecorder)
        {
            _spotifyIntegration = spotifyIntegration;
            _spotifyPlayer = spotifyPlayer;
            _deepgram = deepgram;
            _wake = wake;
            _audioRecorder = audioRecorder;
        }

        public void RecogniseSpeech()
        {
            Listen();
        }

        private void RecognizerSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }

        //private void RecordAudio()
        //{
        //    var outputFolder = Path.Combine(Path.GetTempPath(), "SamsonRecording");
        //    Directory.CreateDirectory(outputFolder);
        //    var outputFilePath = Path.Combine(outputFolder, "recorded.wav");

        //    var waveIn = new WaveInEvent();
        //    WaveFileWriter writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
        //    waveIn.StartRecording();

        //    waveIn.DataAvailable += (s, a) =>
        //    {
        //        writer.Write(a.Buffer, 0, a.BytesRecorded);
        //        if (writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 3)
        //        {
        //            waveIn.StopRecording();
        //        }
        //    };

        //    waveIn.RecordingStopped += (s, a) =>
        //    {
        //        writer?.Dispose();
        //        waveIn.Dispose();
        //    };
        //}

        private async void Listen()
        {
            var listening = true;
            _audioRecorder.StartRecording();
            while (listening) {
                Thread.Sleep(2000);
                _audioRecorder.Save(Audio.AudioFilePath);
                using (AudioFileReader reader = new AudioFileReader(Audio.AudioFilePath))
                {
                    TimeSpan silenceDuration = reader.GetSilenceDuration(AudioRecorder.SilenceLocation.Start);
                    if (silenceDuration.Milliseconds > 2000)
                    {
                        _audioRecorder.StopRecording();
                        listening = false;
                    }
                }
            }

            // send to deepgram
            var transcript = await _deepgram.SpeechToTextFromFile(Audio.AudioFilePath);
            
            // feed transcript to model

            // call a samson action based on whats returned by model

            //finish
        }
    }
}
