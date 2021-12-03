namespace Shared;

public class AnalyzeStatus
{
    // should contain a status with done, in progress, failed, etc.
    // should contain a message with a description of the status
    public bool Success { get; set; }
    public string Message { get; set; }
}