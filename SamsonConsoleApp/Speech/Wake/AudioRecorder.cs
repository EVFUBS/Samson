using NAudio.Wave;
using System.Diagnostics;

namespace SamsonConsoleApp.Speech.Wake
{
    public class AudioRecorder : IAudioRecorder
    {
        public WaveInEvent MyWaveIn;
        public readonly double RecordTime;

        private WaveOutEvent _wav = new WaveOutEvent();
        private bool _isFull = false;
        private int _pos = 0;
        private byte[] _buffer;
        private bool _isRecording = false;
        public enum SilenceLocation { Start, End }

        // Creates a new recorder with a buffer
        public AudioRecorder(double recordTime)
        {
            RecordTime = recordTime;
            MyWaveIn = new WaveInEvent();
            MyWaveIn.DataAvailable += DataAvailable;
            _buffer = new byte[(int)(MyWaveIn.WaveFormat.AverageBytesPerSecond * RecordTime)];
        }

        // Starts recording
        public void StartRecording()
        {
            if (!_isRecording)
            {
                try
                {
                    MyWaveIn.StartRecording();
                }
                catch (InvalidOperationException)
                {
                    Debug.WriteLine("Already recording!");
                }
            }

            _isRecording = true;
        }

        // Stops recording
        public void StopRecording()
        {
            MyWaveIn.StopRecording();
            _isRecording = false;
        }

        // Play currently recorded data
        public void PlayRecorded()
        {
            if (_wav.PlaybackState == PlaybackState.Stopped)
            {
                var buff = new BufferedWaveProvider(MyWaveIn.WaveFormat);
                var bytes = GetBytes();
                buff.AddSamples(bytes, 0, bytes.Length);
                _wav.Init(buff);
                _wav.Play();
            }

        }

        // Stops replay
        public void StopReplay()
        {
            if (_wav != null) _wav.Stop();
        }

        // Save to disk
        public void Save(string fileName)
        {
            var writer = new WaveFileWriter(fileName, MyWaveIn.WaveFormat);
            var buff = GetBytes();
            writer.Write(buff, 0, buff.Length);
            writer.Flush();
            writer.Dispose();
        }


        private void DataAvailable(object sender, WaveInEventArgs e)
        {
            for (int i = 0; i < e.BytesRecorded; ++i)
            {
                _buffer[_pos] = e.Buffer[i];
                _pos = (_pos + 1) % _buffer.Length;
                _isFull |= _pos == 0;
            }
        }

        public byte[] GetBytes()
        {
            int length = _isFull ? _buffer.Length : _pos;
            var bytesToSave = new byte[length];
            int byteCountToEnd = _isFull ? _buffer.Length - _pos : 0;
            if (byteCountToEnd > 0)
            {
                Array.Copy(_buffer, _pos, bytesToSave, 0, byteCountToEnd);
            }
            if (_pos > 0)
            {
                Array.Copy(_buffer, 0, bytesToSave, byteCountToEnd, _pos);
            }
            return bytesToSave;
        }

        /// Starts recording if WaveIn stopped
        private void Stopped(object sender, StoppedEventArgs e)
        {
            Debug.WriteLine("Recording stopped!");
            if (e.Exception != null) Debug.WriteLine(e.Exception.Message);
            if (_isRecording)
            {
                MyWaveIn.StartRecording();
            }
        }
    }
}