namespace SamsonClient.Speech.TextToSpeech
{
    public interface ITextToSpeech
    {
        Task Say(string summary);
    }
}