using System;
using System.Threading.Tasks;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Services;
using Shared;

namespace Unit_Tests;

public class AnalyzeControllerShould
{
    // Setup a AnalyzerController object and mock the IAnalyzeService
    private AnalyzeController _controller;
    private Mock<IAnalyzeService> _mockService;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IAnalyzeService>();
        _controller = new AnalyzeController(_mockService.Object);

        // Setup the mock service to return the expected result
        // The Analyze method will take a AnalyzeRequest object and return a AnalyzeResponse object
        // The Analyze method will return a AnalyzeResponse object with the expected result
        _mockService.Setup(service => service.Analyze(It.IsAny<AnalyzeRequest>()))
            .Returns(Task.FromResult(
                new AnalyzeResponse
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000000"),
                    Status = new AnalyzeStatus
                    {
                        Message = "Success",
                        Success = true
                    },
                    Evaluation = new AnalyzeEvaluation
                    {
                        Prediction = "positive",
                        Probability = 0.9
                    },
                    Request = new AnalyzeRequest
                    {
                        CreatedDate = DateTime.Now,
                        Requestor = "Me",
                        Sentence = "This is a positive test"
                    }
                }
            ));
    }

    // test the Analyze method
    [Test]
    public async Task Analyze_Returns_AnalyzeResponse()
    {
        // Arrange
        var request = new AnalyzeRequest
        {
            CreatedDate = DateTime.Now,
            Requestor = "Me",
            Sentence = "This is a positive test"
        };

        // Act
        var response = await _controller.Analyze(request);
        var okResult = response.Result as OkObjectResult;
        var result = okResult.Value as AnalyzeResponse;

        // Assert
        Assert.IsInstanceOf<AnalyzeResponse>(result);
        Assert.AreEqual(result.Status.Success, true);
        Assert.AreEqual(result.Status.Message, "Success");
        Assert.AreEqual(result.Evaluation.Prediction, "positive");
        Assert.AreEqual(result.Evaluation.Probability, 0.9);
    }
}