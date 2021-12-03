using ML.NET;
using Moq;
using NUnit.Framework;
using Services;
using Shared;

namespace Unit_Tests.Services;

public class AnalyzeServiceShould
{
    //setup method for mocking a mlHandler
    private readonly Mock<MlHandler> _mlHandler;

    public AnalyzeServiceShould()
    {
        _mlHandler = new Mock<MlHandler>();
        _mlHandler.Setup(x => x.Predict(It.IsAny<string>())).Returns(
            new AnalyzeEvaluation()
            {
                Prediction = "Positive",
                Probability = 0.9,
            }
        );
    }

    // Create a unit test for the AnalyzeService.
    // The unit test should test the Analyze method.

    // Test the Analyze method with a AnalyzeRequest object that contains a valid sentence.
    // The unit test should pass.
    // and return a valid AnalyzeResponse object with the AnalyzedSentence property set to the same value as the AnalyzeRequest.Sentence property.
    [Test]
    public void Analyze_ValidSentence_ReturnsValidAnalyzeResponse()
    {
        // Arrange
        var analyzeRequest = new AnalyzeRequest
        {
            Sentence = "This is a test sentence."
        };
        var expectedAnalyzeResponse = new AnalyzeResponse
        {
            Request = analyzeRequest,
        };
        var analyzeService = new AnalyzeService(_mlHandler.Object);

        // Act
        var actualAnalyzeResponse = analyzeService.Analyze(analyzeRequest).Result;

        // Assert
        Assert.IsNotNull(actualAnalyzeResponse);
        Assert.IsNotNull(actualAnalyzeResponse.Request);
        Assert.AreEqual(expectedAnalyzeResponse.Request.Sentence, actualAnalyzeResponse.Request.Sentence);
    }
}