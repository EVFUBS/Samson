using AutoMapper;
using Whisper.net;
using Whisper.net.Ggml;

namespace SamsonServer.Providers.Speech;

public class SpeechProvider : ISpeechProvider
{
    private const string WHISPER_MODEL_FILE = "ggml-base-en.bin";
    
    public async Task<string> SpeechToText(Stream data)
    {
        InstallModelIfNotInstalled();
        
        using var whisperFactory = WhisperFactory.FromPath(WHISPER_MODEL_FILE);
        await using var processor = whisperFactory.CreateBuilder()
            .WithLanguage("auto")
            .Build();

        var transcribedString = "";
        await foreach(var result in processor.ProcessAsync(data))
        {
            transcribedString += result.Text;
        }

        return transcribedString;
    }

    private static async void InstallModelIfNotInstalled()
    {
        if (File.Exists(WHISPER_MODEL_FILE)) return;
        await using var modelStream = await WhisperGgmlDownloader.GetGgmlModelAsync(GgmlType.BaseEn);
        await using var fileWriter = File.OpenWrite(WHISPER_MODEL_FILE);
        await modelStream.CopyToAsync(fileWriter);
    }
}