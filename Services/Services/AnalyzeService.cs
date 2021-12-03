using ML.NET;
using Shared;
using Shared.Internal;

namespace Services;

public class AnalyzeService : IAnalyzeService
{
    private readonly MlHandler _mlHandler;

    public AnalyzeService(MlHandler mlHandler)
    {
        _mlHandler = mlHandler;
    }

    public Task<AnalyzeResponse> Analyze(AnalyzeRequest? request)
    {
        if (ValidateInputSentence(request, out var analyze)) return Task.FromResult(analyze);

        var response = new AnalyzeResponse
        {
            Id = Guid.NewGuid(),
            Request = request,
        };

        try
        {
            var evaluation = _mlHandler.Predict(request.Sentence);
            response.Evaluation = evaluation;
            response.Status = new AnalyzeStatus()
            {
                Message = "Successfully evaluated sentence",
                Success = true,
            };
        }
        catch (Exception ex)
        {
            response.Status = new AnalyzeStatus()
            {
                Message = "Failed to evaluate sentence",
                Success = false,
            };
        }

        return Task.FromResult(response);
    }

    public Task<TrainingEvaluation> Train()
    {
        return Task.FromResult(
            _mlHandler.Train(@"C:\Users\Justin\RiderProjects\Sentiment Anaylzer\ML.NET\Data\yelp_labelled.txt"));
    }

    private static bool ValidateInputSentence(AnalyzeRequest? request, out AnalyzeResponse analyze)
    {
        analyze = new AnalyzeResponse()
        {
            Status = new AnalyzeStatus
            {
                Success = false,
                Message = "Sentence is empty"
            }
        };
        return string.IsNullOrWhiteSpace(request?.Sentence);
    }
}