using Shared;

namespace Services;

public interface IAnalyzeService
{
    public Task<AnalyzeResponse> Analyze(AnalyzeRequest request);
}