namespace SamsonServer.Providers.Speech;

public interface ISpeechProvider
{
    Task<string> SpeechToText(Stream data);
}