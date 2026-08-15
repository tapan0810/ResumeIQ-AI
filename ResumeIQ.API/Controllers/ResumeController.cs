using Microsoft.AspNetCore.Mvc;
using ResumeIQ.API.Models;
using ResumeIQ.API.Services;

namespace ResumeIQ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResumeController : ControllerBase
{
    private readonly IAiService _aiService;

    public ResumeController(IAiService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("analyze")]
    public async Task<ActionResult<ResumeAnalysisResponse>> Analyze(
        [FromBody] ResumeAnalysisRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.ResumeText))
        {
            return BadRequest(new
            {
                message = "Resume text cannot be empty."
            });
        }

        var result = await _aiService.AnalyzeResumeAsync(
            request.ResumeText);

        return Ok(result);
    }
}