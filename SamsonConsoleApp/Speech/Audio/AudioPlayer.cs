using NAudio.Wave;

namespace SamsonConsoleApp.Speech.Audio
{
    public static class AudioPlayer
    {
        public static void playMp3(string filePath)
        {
            var reader = new Mp3FileReader(filePath);
            var waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(reader);
            waveOutEvent.Play();
        }

        public static void playWav(string filePath)
        {
            var reader = new WaveFileReader(filePath);
            var waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(reader);
            waveOutEvent.Play();
        }
    }
}
