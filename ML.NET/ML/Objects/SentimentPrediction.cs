using Microsoft.ML.Data;

namespace ML.NET.ML.Objects;

public class SentimentPrediction
{
    // Create a output class for the sentiment prediction.
    // we need a bool variable to indicate whether the review is positive or negative.
    // we need a float variable to indicate the probability of the review being positive.
    // and we need float for the score.

    [ColumnName("PredictedLabel")] public bool Sentiment { get; set; }

    public float Probability { get; set; }
    public float Score { get; set; }
}