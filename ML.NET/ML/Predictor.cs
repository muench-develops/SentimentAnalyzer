using Microsoft.ML;
using ML.NET.ML.Base;
using ML.NET.ML.Objects;
using Shared;

namespace ML.NET.ML;

internal class Predictor : BaseMl
{
    // create a new predict method
    // taking in a string of text 
    // and returning a prediction
    public AnalyzeEvaluation Predict(string input)
    {
        // check if model file exists
        if (!File.Exists(ModelPath))
        {
            // if not, throw an exception
            throw new Exception("Model file not found");
        }

        using var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read);

        var model = LoadModel(stream);

        var predictionEngine = CreatePredictionEngine(model);

        var prediction = PredictSentiment(input, predictionEngine);

        // Output result
        AnalyzeEvaluation evaluation = new AnalyzeEvaluation
        {
            Prediction = prediction.Sentiment ? "Positive" : "Negative",
            Probability = prediction.Probability
        };

        return evaluation;
    }

    private SentimentPrediction PredictSentiment(string input,
        PredictionEngine<SentimentData, SentimentPrediction> predictionEngine)
    {
        //Make the prediction
        var prediction = predictionEngine.Predict(new SentimentData {SentimentText = input});
        return prediction;
    }

    private PredictionEngine<SentimentData, SentimentPrediction> CreatePredictionEngine(ITransformer? model)
    {
        //Create the predictionEngine
        var predictionEngine = MlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
        return predictionEngine;
    }

    private ITransformer? LoadModel(FileStream stream)
    {
        // load the model
        var model = MlContext.Model.Load(stream, out _);

        //check if model is not null
        if (model == null)
        {
            // if not, throw an exception
            throw new Exception("Model file is empty");
        }

        return model;
    }
}