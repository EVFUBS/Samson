using NAudio.Wave;
using SamsonClient.Speech.Audio;

namespace SamsonClient.Helpers.AudioHelpers
{
    public static class SilenceHelper
    {
        private static bool IsSilence(float amplitude, sbyte threshold)
        {
            var decibels = 20 * Math.Log10(Math.Abs(amplitude));
            return decibels < threshold;
        }
        
        public static void ApplyNoiseGate(float[] buffer, sbyte threshold = -40)
        {
            for (var i = 0; i < buffer.Length; i++)
            {
                if (IsSilence(buffer[i], threshold))
                {
                    buffer[i] = 0;
                }
            }
        }

        public static TimeSpan GetSilenceDuration(this AudioFileReader reader, AudioRecorder.SilenceLocation location, sbyte threshold = -40)
        {
            var counter = 0;
            var volumeFound = false;
            var endOfFile = false;
            var oldPosition = reader.Position;
            var buffer = new float[reader.WaveFormat.SampleRate * 4];
            
            while (!volumeFound && !endOfFile)
            {
                var samplesRead = reader.Read(buffer, 0, buffer.Length);
                if (samplesRead == 0)
                    endOfFile = true;

                for (var n = 0; n < samplesRead; n++)
                {
                    if (IsSilence(buffer[n], threshold))
                    {
                        counter++;
                    }
                    else
                    {
                        if (location == AudioRecorder.SilenceLocation.Start)
                        {
                            volumeFound = true;
                            break;
                        }

                        if (location == AudioRecorder.SilenceLocation.End)
                        {
                            counter = 0;
                        }
                    }
                }
            }

            // reset position
            reader.Position = oldPosition;
            var silenceSamples = (double)counter / reader.WaveFormat.Channels;
            var silenceDuration = (silenceSamples / reader.WaveFormat.SampleRate) * 1000;
            return TimeSpan.FromMilliseconds(silenceDuration);
        }
    }
}
