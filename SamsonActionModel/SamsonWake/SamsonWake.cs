using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Transforms;
using Microsoft.ML.Data;
using Spectrogram;
using System.Drawing;
using System.Drawing.Imaging;

namespace SamsonActionModel.SamsonWake
{
    public class SamsonWake
    {
        // This is going to be converting audio data to Mel Spectograms images and use a convolutional neural network
        // to classify the audio data into 2 catergories, Wake and NotWake

        // Mel Spectograms capture audio data more accurately related to human pitch perception by applying the mel scale
        // to the spectogram

        // If you need to refresh on what a spectrum is read https://medium.com/analytics-vidhya/understanding-the-mel-spectrogram-fca2afa2ce53
        // GitHub for spectogram https://github.com/swharden/Spectrogram
        // Useful example to refer to https://github.com/aslotte/mlnet-sound-classifier/blob/master/src/Program.cs

        private static string _filePath = @"C:\Users\lssmith\Documents\pdrepos\Samson\SamsonConsoleApp\SamsonActionModel\Data\SamsonWakeData\";

        public void Train()
        {
            // get all wav files in the _filePath directory and sub-directories
            string[] allAudioFiles = Directory.GetFiles(_filePath, "*.wav*", SearchOption.AllDirectories);
            foreach (string file in allAudioFiles)
            {
                if (File.Exists(file))
                {
                    (double[] audio, int sampleRate) = ReadMono(file);
                    var spectogramGenerator = new SpectrogramGenerator(sampleRate, fftSize: 4096, stepSize: 500, maxFreq: 3000);
                    spectogramGenerator.Add(audio);
                    //spectogramGenerator.SaveImage(file);
                    Bitmap bitMapMel = spectogramGenerator.GetBitmapMel(melBinCount: 250);
                    bitMapMel.Save(file.Insert(0, "mel"), ImageFormat.Png);
                }
            }

            MLContext mLContext = new MLContext();

            var wakeImagePath = _filePath + @"\Wake";
            var notWakeImagePath = _filePath + @"\NotWake";
            IEnumerable<SpectrogramData> WakeImages = LoadImagesFromDirectory(folder: wakeImagePath, useFolderNameasLabel: false).ToList();
            IEnumerable<SpectrogramData> NotWakeImages = LoadImagesFromDirectory(folder: notWakeImagePath, useFolderNameasLabel: false).ToList();

        }

        (double[] audio, int sampleRate) ReadMono(string filePath, double multiplier = 16_000)
        {
            using var afr = new NAudio.Wave.AudioFileReader(filePath);
            int sampleRate = afr.WaveFormat.SampleRate;
            int bytesPerSample = afr.WaveFormat.BitsPerSample / 8;
            int sampleCount = (int)(afr.Length / bytesPerSample);
            int channelCount = afr.WaveFormat.Channels;
            var audio = new List<double>(sampleCount);
            var buffer = new float[sampleRate * channelCount];
            int samplesRead = 0;
            while ((samplesRead = afr.Read(buffer, 0, buffer.Length)) > 0)
                audio.AddRange(buffer.Take(samplesRead).Select(x => x * multiplier));
            return (audio.ToArray(), sampleRate);
        }

        public static IEnumerable<SpectrogramData> LoadImagesFromDirectory(string folder, bool useFolderNameasLabel = true)
        {
            var files = Directory.GetFiles(folder, "mel*",
                searchOption: SearchOption.AllDirectories);
        }
    }
}
