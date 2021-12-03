using Microsoft.AspNetCore.Mvc;
using Services;
using Shared;
using Shared.Internal;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AnalyzeController : ControllerBase
{
    private readonly IAnalyzeService _analyzeService;

    public AnalyzeController(IAnalyzeService analyzeService)
    {
        _analyzeService = analyzeService;
    }

    // POST : api/Analyze takes  a json object with the following properties:
    // a sentence to analyze with the _analyzeService

    [HttpPost("analyze")]
    public async Task<ActionResult<AnalyzeResponse>> Analyze([FromBody] AnalyzeRequest? request)
    {
        // if the request is null, return bad request
        if (request == null)
        {
            return BadRequest("Request is null");
        }

        AnalyzeResponse response = await _analyzeService.Analyze(request);

        return Ok(response);
    }

    [HttpPost("train")]
    public async Task<ActionResult<TrainingEvaluation>> Train()
    {
        var response = await _analyzeService.Train();

        return Ok(response);
    }
}