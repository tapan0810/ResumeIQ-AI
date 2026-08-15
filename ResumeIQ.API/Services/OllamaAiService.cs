using System.Net.Http.Json;
using System.Text.Json;
using ResumeIQ.API.Models;

namespace ResumeIQ.API.Services;

public class OllamaAiService : IAiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public OllamaAiService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<ResumeAnalysisResponse> AnalyzeResumeAsync(
        string resumeText)
    {
        var model = _configuration["Ollama:Model"] ?? "gemma3";

        var prompt = """
        You are an AI resume analyzer.

        Analyze the following resume text.

        Identify:

        1. Skills
        2. Strengths
        3. Weaknesses
        4. Recommendations

        Return ONLY valid JSON.

        The JSON must have exactly this structure:

        {
            "skills": [],
            "strengths": [],
            "weaknesses": [],
            "recommendations": []
        }

        Resume:
        """ + resumeText;

        var request = new
        {
            model = model,
            prompt = prompt,
            stream = false
        };

        var response = await _httpClient.PostAsJsonAsync(
            "/api/generate",
            request);

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<OllamaResponse>();

        if (result == null || string.IsNullOrWhiteSpace(result.Response))
        {
            throw new InvalidOperationException(
                "Ollama returned an empty response.");
        }

        var analysis = JsonSerializer.Deserialize<ResumeAnalysisResponse>(
            result.Response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (analysis == null)
        {
            throw new InvalidOperationException(
                "Unable to parse AI response.");
        }

        return analysis;
    }

    private class OllamaResponse
    {
        public string Response { get; set; } = string.Empty;
    }
}