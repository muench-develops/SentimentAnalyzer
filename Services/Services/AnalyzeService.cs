using Shared;

namespace Services;

public class AnalyzeService : IAnalyzeService
{
    public AnalyzeService()
    {
    }

    public async Task<AnalyzeResponse> Analyze(AnalyzeRequest request)
    {
        if (ValidateInputSentence(request, out var analyze)) return analyze;

        var response = new AnalyzeResponse
        {
            Request = request
        };

        return response;
    }

    private static bool ValidateInputSentence(AnalyzeRequest request, out AnalyzeResponse analyze)
    {
        analyze = new AnalyzeResponse()
        {
            Status = new AnalyzeStatus
            {
                Success = false,
                Message = "Sentence is empty"
            }
        };
        return string.IsNullOrWhiteSpace(request.Sentence);
    }
}