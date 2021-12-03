namespace Shared;

public class AnalyzeResponse
{
    // The response object for the analyze request.
    // Should contain the Request, the Status of the request,
    // the evaluation of the request.
    public Guid Id { get; set; }
    public AnalyzeRequest? Request { get; set; }
    public AnalyzeStatus Status { get; set; }
    public AnalyzeEvaluation Evaluation { get; set; }
}