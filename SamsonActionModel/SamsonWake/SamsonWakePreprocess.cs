using NAudio.Wave;
using Spectrogram;
using System.Drawing;
using System.Drawing.Imaging;

namespace SamsonActionModel.SamsonWake
{
    public class SamsonWakePreprocess
    {
        private static string _notWakeFilePath = @"C:\Users\lssmith\Documents\pdrepos\Samson\SamsonConsoleApp\SamsonActionModel\Data\SamsonWakeData\OriginalData\NotWake\";
        private static string _notWakeSpectogramFilePath = @"C:\Users\lssmith\Documents\pdrepos\Samson\SamsonConsoleApp\SamsonActionModel\Data\SamsonWakeData\TrainData\NotWake\";
        private static string _wakeFilePath = @"C:\Users\lssmith\Documents\pdrepos\Samson\SamsonConsoleApp\SamsonActionModel\Data\SamsonWakeData\OriginalData\Wake\";
        private static string _wakeSpectogramFilePath = @"C:\Users\lssmith\Documents\pdrepos\Samson\SamsonConsoleApp\SamsonActionModel\Data\SamsonWakeData\TrainData\Wake\";

        public void Preprocess()
        {
            // get all wav files in the _filePath directory and sub-directories
            string[] notWakeAudioFiles = Directory.GetFiles(_notWakeFilePath, "*.wav*", SearchOption.AllDirectories);
            string[] wakeAudioFiles = Directory.GetFiles(_wakeFilePath, "*.wav", SearchOption.AllDirectories);
            CreateMelSpectograms(notWakeAudioFiles, _notWakeSpectogramFilePath, "NotWake");
            CreateMelSpectograms(wakeAudioFiles, _wakeSpectogramFilePath, "Wake");
        }

        public void CreateMelSpectograms(string[] audioFiles, string savePath, string filePrefix)
        {
            int i = 1;
            foreach (string file in audioFiles)
            {
                if (File.Exists(file))
                {
                    (double[] audio, int sampleRate) = ReadMono(file);
                    var bitMapMel = GenerateMelSpectogram(audio, sampleRate);
                    var filename = savePath + $"{filePrefix}{i}.png";
                    bitMapMel.Save(filename, ImageFormat.Jpeg);
                    i++;
                }
            }
        }

        public Bitmap CreateMelSpectogramFromStream(Stream data)
        {
            WaveFormat waveFormat = new WaveFormat(44100, 16, 2);
            using (var waveStream = new RawSourceWaveStream(data, waveFormat))
            {
                (double[] audio, int sampleRate) = ReadMono(waveStream);
                var bitMapMel = GenerateMelSpectogram(audio, sampleRate);
                return bitMapMel;
            }
        }

        private Bitmap GenerateMelSpectogram(double[] audio, int sampleRate)
        {
            var spectogramGenerator = new SpectrogramGenerator(sampleRate, fftSize: 4096, stepSize: 500, maxFreq: 3000);
            spectogramGenerator.Add(audio);
            return spectogramGenerator.GetBitmapMel(melBinCount: 250);
        }

        (double[] audio, int sampleRate) ReadMono(string filePath, double multiplier = 16_000)
        {
            using var afr = new AudioFileReader(filePath);
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

        (double[] audio, int sampleRate) ReadMono(RawSourceWaveStream? data, double multiplier = 16_000)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var waveFormat = data.WaveFormat;
            int sampleRate = waveFormat.SampleRate;
            int bytesPerSample = waveFormat.BitsPerSample / 8;
            int sampleCount = (int)(data.Length / bytesPerSample);
            int channelCount = waveFormat.Channels;
            var audio = new List<double>(sampleCount);
            var buffer = new float[sampleRate * channelCount];
            int samplesRead = 0;
            while ((samplesRead = data.ToSampleProvider().Read(buffer, 0, buffer.Length)) > 0)
                audio.AddRange(buffer.Take(samplesRead).Select(x => x * multiplier));
            return (audio.ToArray(), sampleRate);
        }

        public static IEnumerable<SpectrogramData> LoadImagesFromDirectory(string folder, bool useFolderNameasLabel = true)
        {
            var files = Directory.GetFiles(folder, "mel*",
                searchOption: SearchOption.AllDirectories);

            foreach (var file in files)
            {
                if ((Path.GetExtension(file) != ".jpg") && (Path.GetExtension(file) != ".png"))
                    continue;

                var fileName = Path.GetFileName(file);
                var label = fileName.Substring(0, fileName.LastIndexOf("_"));

                yield return new SpectrogramData()
                {
                    ImagePath = file,
                    Label = label
                };
            }
        }
    }
}
