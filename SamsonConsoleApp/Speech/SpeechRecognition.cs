using NAudio.Wave;
using SamsonAIClient;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Helpers.AudioHelpers;
using SamsonConsoleApp.Speech.Deepgram;
using SamsonConsoleApp.Helpers;
using Deepgram.Models;
using SamsonConsoleApp.Speech.Audio;
using SamsonConsoleApp.Constants;
using SamsonConsoleApp.Execute.ExecuteActions;

namespace SamsonConsoleApp.Speech
{
    public class SpeechRecognition : ISpeechRecognition
    {
        private readonly IAiClientFactory _samsonClientFactory;
        private readonly ISpeechDeepgram _deepgram;
        private readonly IExecuteAction _executeAction;

        public SpeechRecognition(
            IAiClientFactory AiClientFactory,
            ISpeechDeepgram deepgram,
            IExecuteAction executeAction)
        {
            _samsonClientFactory = AiClientFactory;
            _deepgram = deepgram;
            _executeAction = executeAction;
        }

        public async Task Start()
        {
            while (true)
            {
                Logger.Log("Waiting for wake...");
                await Wake(AudioFilePaths.WakeAudioFilePath, 5, 2000);
                
                Logger.Log("Listening...");
                await Listen(AudioFilePaths.ListenAudioFilePath, 120, 5000, 300);

                AudioRecorder.Concatenate(AudioFilePaths.FullAudioFilePath, new List<string>
                {
                    AudioFilePaths.WakeAudioFilePath,
                    AudioFilePaths.ListenAudioFilePath
                });

                Logger.Log("Sending audio to deepgram");
                var transcript = await _deepgram.SpeechToTextFromFile(AudioFilePaths.FullAudioFilePath);

                Logger.Log("Sending following transcript to samson actions: ", transcript.Results.Summary.TextSummary);
                var client = _samsonClientFactory.Create();
                var response = await client.GetSamsonActionAsync(new SamsonActionRequest
                {
                    Summary = transcript.Results.Summary.TextSummary
                });

                Logger.Log($"Recieved action: {nameof(response.Action)}. Executing Action...");
                _executeAction.Execute(response.ToAction(), transcript.Results.Summary.TextSummary);
                Logger.Log("Execution Complete!\n");
            }
        }

        public async Task TestStart()
        {
            var summary = "This is the text summary";
            var request = new SamsonActionRequest { Summary = summary};
            var client = _samsonClientFactory.Create();
            var response = await client.GetSamsonActionAsync(request);
            _executeAction.Execute(response.ToAction(), summary);
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
