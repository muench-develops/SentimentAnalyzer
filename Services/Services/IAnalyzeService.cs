using Shared;
using Shared.Internal;

namespace Services;

public interface IAnalyzeService
{
    public Task<AnalyzeResponse> Analyze(AnalyzeRequest? request);
    public Task<TrainingEvaluation> Train();
}