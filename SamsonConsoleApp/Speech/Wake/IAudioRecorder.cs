public interface IAudioRecorder
{
    byte[] GetBytes();
    void PlayRecorded();
    void Save(string fileName);
    void StartRecording();
    void StopRecording();
    void StopReplay();
}