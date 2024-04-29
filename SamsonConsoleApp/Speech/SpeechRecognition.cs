using NAudio.Wave;
using SamsonAIClient;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Helpers.AudioHelpers;
using SamsonConsoleApp.Helpers;
using SamsonConsoleApp.Speech.Audio;
using SamsonConsoleApp.Constants;
using SamsonServerClient;
using SamsonConsoleApp.Execute;

namespace SamsonConsoleApp.Speech
{
    public class SpeechRecognition(
        IAiClientFactory aiClientFactory,
        IServerClientFactory serverClientFactory,
        IActionCollection actionCollection) : ISpeechRecognition
    {

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
                var transcript = await GetTranscript();

                Logger.Log("Sending following transcript to samson actions: ", transcript.Results.Summary.TextSummary);
                var response = await GetAction(transcript);

                Logger.Log($"Recieved action: {nameof(response.Action)}. Executing Action...");
                actionCollection.Execute(response.ToAction());
                Logger.Log("Execution Complete!\n");
            }
        }

        public async Task TestStart()
        {
            var summary = "pause this song";
            var client = serverClientFactory.Create();
            try
            {
                var response = await client.ActionAsync(summary);
                actionCollection.Execute(response.ToAction());
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        private async Task Wake(string audioFilePath, double recordTime, int listenTimeInMilliseconds)
        {
            var samsonClient = aiClientFactory.Create();
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

        private async Task<PrerecordedTranscription> GetTranscript()
        {
            var serverClient = serverClientFactory.Create();
            using (FileStream fileStream = File.OpenRead(AudioFilePaths.FullAudioFilePath))
            {
                return await serverClient.SynthAsync(fileStream.ToStream());
            }
        }

        private async Task<SamsonAction> GetAction(PrerecordedTranscription transcript)
        {
            var client = serverClientFactory.Create();
            var response = await client.ActionAsync(transcript.Results.Summary.TextSummary);
            return response;
        }
    }
}
