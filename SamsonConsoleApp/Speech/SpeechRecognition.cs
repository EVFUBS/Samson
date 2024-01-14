using NAudio.Wave;
using SamsonConsoleApp.Actions.Spotfiy.Interfaces;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Constants;
using SamsonConsoleApp.Helpers.AudioHelpers;
using SamsonConsoleApp.Speech.Deepgram;
using SamsonConsoleApp.Speech.Wake;
namespace SamsonConsoleApp.Speech
{
    public class SpeechRecognition : ISpeechRecognition
    {
        private readonly ISamsonAIClientFactory _samsonClientFactory;

        public SpeechRecognition(
            ISamsonAIClientFactory samsonClientFactory)
        {
            _samsonClientFactory = samsonClientFactory;
        }

        public async Task Start()
        {
            while (true) {
                await Wake(Audio.WakeAudioFilePath);
                await ListenAsync(Audio.ListenAudioFilePath, 5000, 1000);

                // append wake and listen together
                
                // send to deepgram

                // send to samsonAction endpoint

                // call action based on what is returned
            }
        }

        private async Task Wake(string audioFilePath)
        {
            var samsonClient = _samsonClientFactory.Create();
            var listening = true;
            var wakeRecorder = new AudioRecorder(2);
            wakeRecorder.StartRecording();

            while (listening)
            {
                await Task.Delay(2000);
                wakeRecorder.Save(audioFilePath);

                using (var fileStream = File.OpenRead(audioFilePath))
                {
                    var response = await samsonClient.GetSamsonWake_api_wake_postAsync(new SamsonAIClient.FileParameter(fileStream));
                    Console.WriteLine(response.Wake);
                    if (response.Wake == true)
                    {
                        wakeRecorder.StopRecording();
                        break;
                    }
                }
            }
        }

        private async Task ListenAsync(string audioFilePath, int listenTimeInMilliseconds, double silenceDurationInMilliseconds)
        {
            var listening = true;
            var actionRecorder = new AudioRecorder(120);
            actionRecorder.StartRecording();

            while (listening)
            {
                await Task.Delay(listenTimeInMilliseconds);

                actionRecorder.Save(audioFilePath);

                using (var reader = new AudioFileReader(audioFilePath))
                {
                    TimeSpan silenceDuration = reader.GetSilenceDuration(AudioRecorder.SilenceLocation.Start);

                    if (silenceDuration.TotalMilliseconds > silenceDurationInMilliseconds)
                    {
                        actionRecorder.StopRecording();
                        listening = false;
                    }
                }
            }
        }
    }
}
