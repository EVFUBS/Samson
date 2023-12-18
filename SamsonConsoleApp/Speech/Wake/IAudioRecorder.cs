public interface IAudioRecorder
{
    byte[] GetBytesToSave();
    void PlayRecorded();
    void Save(string fileName);
    void StartRecording();
    void StopRecording();
    void StopReplay();
}