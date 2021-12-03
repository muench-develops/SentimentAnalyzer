using ML.NET.ML;
using Shared;
using Shared.Internal;

namespace ML.NET;

public class MlHandler
{
    public TrainingEvaluation Train(string filePath)
    {
        Trainer trainer = new Trainer();
        return trainer.Train(filePath);
    }

    public AnalyzeEvaluation Predict(string input)
    {
        Predictor predictor = new Predictor();
        return predictor.Predict(input);
    }
}