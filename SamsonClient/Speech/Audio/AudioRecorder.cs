using NAudio.Wave;
using System.Diagnostics;
using NAudio.Utils;
using SamsonClient.Helpers.AudioHelpers;

namespace SamsonClient.Speech.Audio
{
    public class AudioRecorder : IAudioRecorder
    {
        private readonly WaveInEvent _waveInEvent;
        private readonly WaveOutEvent _waveOutEvent = new();
        private bool _isFull;
        private int _currentIndex;
        private readonly byte[] _buffer;
        private bool _isRecording;
        private const int SAMPLE_RATE = 16000;
        private const int CHANNELS = 1;

        public enum SilenceLocation
        {
            Start,
            End
        }

        public AudioRecorder(double recordTime)
        {
            _waveInEvent = new WaveInEvent{ WaveFormat = new WaveFormat(SAMPLE_RATE, CHANNELS) };
            _waveInEvent.DataAvailable += DataAvailable;
            _buffer = new byte[(int)(_waveInEvent.WaveFormat.AverageBytesPerSecond * recordTime)];
        }

        public void StartRecording()
        {
            if (!_isRecording)
            {
                try
                {
                    _waveInEvent.StartRecording();
                }
                catch (InvalidOperationException)
                {
                    Debug.WriteLine("Already recording!");
                }
            }

            _isRecording = true;
        }

        public void StopRecording()
        {
            _waveInEvent.StopRecording();
            _isRecording = false;
        }

        public void PlayRecorded()
        {
            if (_waveOutEvent.PlaybackState != PlaybackState.Stopped) return;
            var buff = new BufferedWaveProvider(_waveInEvent.WaveFormat);
            var bytes = GetBytes();
            buff.AddSamples(bytes, 0, bytes.Length);
            _waveOutEvent.Init(buff);
            _waveOutEvent.Play();

        }

        public void StopReplay()
        {
            _waveOutEvent.Stop();
        }

        public void Save(string fileName)
        {
            var writer = new WaveFileWriter(fileName, _waveInEvent.WaveFormat);
            var buff = GetBytes();
            writer.Write(buff, 0, buff.Length);
            writer.Flush();
            writer.Dispose();
        }


        private void DataAvailable(object sender, WaveInEventArgs @event)
        {
            var floatBuffer = new float[@event.BytesRecorded / 4];
            Buffer.BlockCopy(@event.Buffer, 0, floatBuffer, 0, @event.BytesRecorded);
            SilenceHelper.ApplyNoiseGate(floatBuffer);
            Buffer.BlockCopy(floatBuffer, 0, @event.Buffer, 0, @event.BytesRecorded);

            
            for (var i = 0; i < @event.BytesRecorded; ++i)
            {
                _buffer[_currentIndex] = @event.Buffer[i];
                _currentIndex = (_currentIndex + 1) % _buffer.Length;
                _isFull |= _currentIndex == 0;
            }
        }

        public byte[] GetBytes()
        {
            var length = _isFull ? _buffer.Length : _currentIndex;
            var bytesToSave = new byte[length];
            var byteCountToEnd = _isFull ? _buffer.Length - _currentIndex : 0;
            
            if (byteCountToEnd > 0)
            {
                Array.Copy(_buffer, _currentIndex, bytesToSave, 0, byteCountToEnd);
            }
            
            if (_currentIndex > 0)
            {
                Array.Copy(_buffer, 0, bytesToSave, byteCountToEnd, _currentIndex);
            }
            
            return bytesToSave;
        }

        public static void Concatenate(string outputFile, IEnumerable<string> sourceFiles)
        {
            var buffer = new byte[1024];
            WaveFileWriter? waveFileWriter = null;

            try
            {
                foreach (var sourceFile in sourceFiles)
                {
                    using var reader = new WaveFileReader(sourceFile);
                    if (waveFileWriter == null)
                    {
                        waveFileWriter = new WaveFileWriter(outputFile, reader.WaveFormat);
                    }
                    else
                    {
                        if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat))
                        {
                            throw new InvalidOperationException("Can't concatenate WAV Files that don't share the same format");
                        }
                    }

                    int read;
                    while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        waveFileWriter.Write(buffer, 0, read);
                    }
                }
            }
            finally
            {
                waveFileWriter?.Dispose();
            }

        }
    }
}