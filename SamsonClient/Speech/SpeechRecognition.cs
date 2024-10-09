using NAudio.Wave;
using SamsonClient.Helpers.AudioHelpers;
using SamsonServerClient;
using AutoMapper;
using SamsonClient.Clients.Interfaces;
using SamsonClient.Constants;
using SamsonClient.Execute;
using SamsonClient.Helpers;
using SamsonClient.Models;
using SamsonClient.Speech.Audio;
using Stream = SamsonServerClient.Stream;

namespace SamsonClient.Speech
{
    public class SpeechRecognition(
        IServerClientFactory serverClientFactory,
        IActionCollection actionCollection) : ISpeechRecognition
    {
        public async Task Start()
        {
            while (true)
            {
                await Wake();
                await Listen();

                AudioRecorder.Concatenate(AudioFilePaths.FullAudioFilePath, new List<string>
                {
                    AudioFilePaths.WakeAudioFilePath,
                    AudioFilePaths.ListenAudioFilePath
                });

                Logger.Log("Converting audio to text");
                var speechText = await ConvertSpeechToText();
                
                Logger.Log(speechText.Text);

                Logger.Log("Sending following transcript to samson actions: ", speechText.Text);
                var response = await GetAction(speechText);

                Logger.Log($"Received action: {nameof(response.Action)}. Executing Action...");
                actionCollection.Execute(response.ToAction());
                
                Logger.Log("Execution Complete!\n");
            }
        }
        
        private async Task<SpeechToText> ConvertSpeechToText()
        {
            var serverClient = serverClientFactory.Create();
            var base64EncodedString = Convert.ToBase64String(await File.ReadAllBytesAsync(AudioFilePaths.FullAudioFilePath)); 
            return await serverClient.SttAsync(new Base64EncodedRequest { FileData = base64EncodedString });
        }

        private async Task<SamsonAction> GetAction(SpeechToText speechToText)
        {
            var client = serverClientFactory.Create();
            var response = await client.ActionAsync(speechToText.Text);
            return response;
        }

        private async Task Wake()
        {
            Logger.Log("Waiting for wake...");
            var listenInfo = new ListenInfo(AudioFilePaths.WakeAudioFilePath, 5, 1000, null);
            await RecordWakeAudioAsync(listenInfo);
        }

        private async Task Listen()
        {
            Logger.Log("Listening...");
            var listenInfo = new ListenInfo(AudioFilePaths.ListenAudioFilePath, 120.0, 5000, 300.0);
            await RecordListenAudioAsync(listenInfo);
        }
        
        private async Task RecordWakeAudioAsync(ListenInfo info)
        {
            var audioRecorder = new AudioRecorder(info.RecordTime);
            audioRecorder.StartRecording();
            await RecordAudioUntilIsWakeAsync(audioRecorder, info);
        }

        private static async Task RecordListenAudioAsync(ListenInfo info)
        {
            var audioRecorder = new AudioRecorder(info.RecordTime);
            audioRecorder.StartRecording();
            await RecordAudioUntilSilenceAsync(audioRecorder, info);
        }

        private async Task RecordAudioUntilIsWakeAsync(AudioRecorder audioRecorder, ListenInfo info)
        {
            var client = serverClientFactory.Create();
            while (true)
            {
                await WaitAndSaveAudio(audioRecorder, info);
                var base64EncodedString = Convert.ToBase64String(await File.ReadAllBytesAsync(info.AudioFilePath)); 
                var response = await client.WakeAsync(new Base64EncodedRequest{ FileData = base64EncodedString });
                if (!response.IsWake) continue;
                audioRecorder.StopRecording();
                break;
            }
        }

        private static async Task RecordAudioUntilSilenceAsync(AudioRecorder audioRecorder, ListenInfo info)
        {
            while (true)
            {
                await WaitAndSaveAudio(audioRecorder, info);
                await using var audioFileReader = new AudioFileReader(info.AudioFilePath);
                var silenceDuration = audioFileReader.GetSilenceDuration(AudioRecorder.SilenceLocation.Start);
                if (!(silenceDuration.TotalMilliseconds > info.SilenceDurationInMilliseconds)) continue;
                audioRecorder.StopRecording();
                break;
            }
        }

        private static async Task WaitAndSaveAudio(AudioRecorder audioRecorder, ListenInfo info)
        {
            await Task.Delay(info.ListenTimeInMilliseconds);
            audioRecorder.Save(info.AudioFilePath);
        }
    }
}
