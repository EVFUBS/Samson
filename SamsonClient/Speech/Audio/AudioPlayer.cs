using NAudio.Wave;

namespace SamsonClient.Speech.Audio
{
    public static class AudioPlayer
    {
        public static void PlayMp3(string filePath)
        {
            var reader = new Mp3FileReader(filePath);
            var waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(reader);
            waveOutEvent.Play();
        }

        public static void PlayWav(string filePath)
        {
            var reader = new WaveFileReader(filePath);
            var waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(reader);
            waveOutEvent.Play();
        }
    }
}
