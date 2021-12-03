using NUnit.Framework;
using Services;
using Shared;

namespace Unit_Tests.Services;

public class AnalyzeServiceShould
{
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
        var analyzeService = new AnalyzeService();

        // Act
        var actualAnalyzeResponse = analyzeService.Analyze(analyzeRequest).Result;

        // Assert
        Assert.IsNotNull(actualAnalyzeResponse);
        Assert.IsNotNull(actualAnalyzeResponse.Request);
        Assert.AreEqual(expectedAnalyzeResponse.Request.Sentence, actualAnalyzeResponse.Request.Sentence);
    }
}