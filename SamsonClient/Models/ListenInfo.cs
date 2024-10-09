namespace SamsonClient.Models;

public record ListenInfo(
    string AudioFilePath,
    double RecordTime,
    int ListenTimeInMilliseconds,
    double? SilenceDurationInMilliseconds);