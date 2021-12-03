using Microsoft.ML.Data;

namespace ML.NET.ML.Objects;

public class SentimentData
{
    // Lets create a new class called SentimentData.
    // This class will contain the data we want to train on.
    // The class will have two properties:
    // 1. SentimentText: This will contain the text of the review.
    // 2. Sentiment: This will contain the sentiment of the review.
    // The Sentiment property will be a boolean value.
    // If the sentiment is positive, then the value will be true.
    // If the sentiment is negative, then the value will be false.

    [LoadColumn(0)] public string SentimentText { get; set; }

    [LoadColumn(1)] public bool Sentiment { get; set; }
}