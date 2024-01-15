using NAudio.Wave;
using SamsonAIClient;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Constants;
using SamsonConsoleApp.Helpers.AudioHelpers;
using SamsonConsoleApp.Speech.Deepgram;
using SamsonConsoleApp.Speech.Wake;
using SamsonConsoleApp.Helpers;
using SamsonConsoleApp.enums;
using SamsonConsoleApp.Actions.Execute;
namespace SamsonConsoleApp.Speech
{
    public class SpeechRecognition : ISpeechRecognition
    {
        private readonly ISamsonAIClientFactory _samsonClientFactory;
        private readonly ISpeechDeepgram _deepgram;
        private readonly IExecuteSamsonAction _executeSamsonAction;

        public SpeechRecognition(
            ISamsonAIClientFactory samsonClientFactory,
            ISpeechDeepgram deepgram,
            IExecuteSamsonAction executeSamsonAction)
        {
            _samsonClientFactory = samsonClientFactory;
            _deepgram = deepgram;
            _executeSamsonAction = executeSamsonAction;
        }

        public async Task Start()
        {
            while (true) {
                await Wake(Audio.WakeAudioFilePath, 5, 2000);
                await Listen(Audio.ListenAudioFilePath, 120, 5000, 1000);

                // append wake and listen together. - This should be done with one recorder, needs to change in the future
                AudioRecorder.Concatenate(Audio.FullAudioFilePath, new List<string>{ Audio.ListenAudioFilePath, Audio.WakeAudioFilePath});

                var transcript = await _deepgram.SpeechToTextFromFile(Audio.FullAudioFilePath);

                var client = _samsonClientFactory.Create();
                var response = await client.GetSamsonActionAsync(new SamsonActionRequest
                {
                    Summary = transcript.Results.Summary.TextSummary
                });

                _executeSamsonAction.Execute(response);
            }
        }

        private async Task Wake(string audioFilePath, double recordTime, int listenTimeInMilliseconds)
        {
            var samsonClient = _samsonClientFactory.Create();
            var listening = true;
            var wakeRecorder = new AudioRecorder(recordTime);
            wakeRecorder.StartRecording();

            while (listening)
            {
                await Task.Delay(listenTimeInMilliseconds);
                wakeRecorder.Save(audioFilePath);

                // want to add a check if the clip is silent dont send will reduce the amount of api calls
                using (var fileStream = File.OpenRead(audioFilePath))
                {
                    var response = await samsonClient.GetSamsonWakeAsync(new FileParameter(fileStream));
                    if (response.Wake == true)
                    {
                        wakeRecorder.StopRecording();
                        break;
                    }
                }
            }
        }

        private async Task Listen(string audioFilePath, double recordTime, int listenTimeInMilliseconds, double silenceDurationInMilliseconds)
        {
            var listening = true;
            var actionRecorder = new AudioRecorder(recordTime);
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
