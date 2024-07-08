using NAudio.Wave;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Helpers.AudioHelpers;
using SamsonConsoleApp.Helpers;
using SamsonConsoleApp.Speech.Audio;
using SamsonConsoleApp.Constants;
using SamsonServerClient;
using SamsonConsoleApp.Execute;
using AutoMapper;

namespace SamsonConsoleApp.Speech
{
    public class SpeechRecognition(
        IServerClientFactory serverClientFactory,
        IActionCollection actionCollection,
        IMapper mapper) : ISpeechRecognition
    {
        public async Task Start()
        {
            Logger.Log("Listening...");
            await Listen(AudioFilePaths.ListenAudioFilePath, 120, 5000, 300);

            AudioRecorder.Concatenate(AudioFilePaths.FullAudioFilePath, new List<string>
            {
                AudioFilePaths.WakeAudioFilePath,
                AudioFilePaths.ListenAudioFilePath
            });

            Logger.Log("Converting audio to transcript");
            var transcript = await GetTranscript();

            Logger.Log("Sending following transcript to samson actions: ", transcript.Results.Summary.TextSummary);
            var response = await GetAction(transcript);

            Logger.Log($"Received action: {nameof(response.Action)}. Executing Action...");
            actionCollection.Execute(response.ToAction());
            Logger.Log("Execution Complete!\n");
        }
        
        public async Task WakeWordStart()
        {
            while (true)
            {
                Logger.Log("Waiting for wake...");
                await Wake(AudioFilePaths.WakeAudioFilePath, 5, 2000);
                await Start();
            }
        }

        // to be continued
        public async Task HotKeyStart()
        {
            while (true)
            {
                if (!Console.KeyAvailable) continue;
                if (Console.ReadKey().Key.Equals(ConsoleKey.LeftWindows) && Console.ReadKey().Key.Equals(ConsoleKey.Q))
                {
                    await Start();
                }
            }
        }
        
        public async Task TestWake()
        {
            await Wake(AudioFilePaths.WakeAudioFilePath, 5, 2000);
            Console.WriteLine("I Woke");
        }

        public async Task TestAction()
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
            var client = serverClientFactory.Create();
            var wakeRecorder = new AudioRecorder(recordTime);
            wakeRecorder.StartRecording();

            while (true)
            {
                await Task.Delay(listenTimeInMilliseconds);
                wakeRecorder.Save(audioFilePath);

                await using var fileStream = File.OpenRead(audioFilePath);
                var response = await client.WakeAsync(mapper.Map<FileStream, SamsonServerClient.Stream>(fileStream));
                if (!response.IsWake) continue;
                wakeRecorder.StopRecording();
                break;
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

                await using var reader = new AudioFileReader(audioFilePath);
                var silenceDuration = reader.GetSilenceDuration(AudioRecorder.SilenceLocation.Start);
                if (!(silenceDuration.TotalMilliseconds > silenceDurationInMilliseconds)) continue;
                actionRecorder.StopRecording();
                listening = false;
            }
        }

        private async Task<PrerecordedTranscription> GetTranscript()
        {
            var serverClient = serverClientFactory.Create();
            await using var fileStream = File.OpenRead(AudioFilePaths.FullAudioFilePath);
            return await serverClient.SynthAsync(fileStream.ToStream());
        }

        private async Task<SamsonAction> GetAction(PrerecordedTranscription transcript)
        {
            var client = serverClientFactory.Create();
            var response = await client.ActionAsync(transcript.Results.Summary.TextSummary);
            return response;
        }
    }
}
