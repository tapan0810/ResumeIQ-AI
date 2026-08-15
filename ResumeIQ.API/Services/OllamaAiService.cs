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
        if (string.IsNullOrWhiteSpace(resumeText))
        {
            throw new ArgumentException(
                "Resume text cannot be empty.",
                nameof(resumeText));
        }

        var model = _configuration["Ollama:Model"] ?? "gemma3";

        var prompt = """
            You are an AI resume analyzer.

            Analyze the following resume.

            Identify:

            1. Skills
            2. Strengths
            3. Weaknesses
            4. Recommendations

            IMPORTANT:
            Return ONLY valid JSON.
            Do NOT use markdown.
            Do NOT use code fences.
            Do NOT include any explanation before or after the JSON.

            The JSON must have exactly this structure:

            {
                "skills": [],
                "strengths": [],
                "weaknesses": [],
                "recommendations": []
            }

            Resume:
            """;

        prompt += Environment.NewLine;
        prompt += resumeText;

        var request = new
        {
            model = model,
            prompt = prompt,
            stream = false,
            format = "json"
        };

        HttpResponseMessage response;

        try
        {
            response = await _httpClient.PostAsJsonAsync(
                "/api/generate",
                request);
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException(
                "Unable to connect to Ollama. Make sure Ollama is running.",
                ex);
        }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            throw new InvalidOperationException(
                $"Ollama returned HTTP {(int)response.StatusCode}: {error}");
        }

        var result = await response.Content
            .ReadFromJsonAsync<OllamaResponse>();

        if (result == null)
        {
            throw new InvalidOperationException(
                "Ollama returned an empty response.");
        }

        if (string.IsNullOrWhiteSpace(result.Response))
        {
            throw new InvalidOperationException(
                "Ollama returned an empty AI response.");
        }

        Console.WriteLine();
        Console.WriteLine("========== RAW OLLAMA RESPONSE ==========");
        Console.WriteLine(result.Response);
        Console.WriteLine("==========================================");
        Console.WriteLine();

        var cleanedResponse = CleanJsonResponse(result.Response);

        try
        {
            var analysis =
                JsonSerializer.Deserialize<ResumeAnalysisResponse>(
                    cleanedResponse,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (analysis == null)
            {
                throw new InvalidOperationException(
                    "AI response could not be converted to the expected format.");
            }

            return analysis;
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                "Ollama returned invalid JSON. " +
                $"Raw response: {result.Response}",
                ex);
        }
    }

    private static string CleanJsonResponse(string response)
    {
        response = response.Trim();

        // Remove markdown code fence if the model still returns one.
        if (response.StartsWith(
                "```json",
                StringComparison.OrdinalIgnoreCase))
        {
            response = response["```json".Length..];
        }
        else if (response.StartsWith("```"))
        {
            response = response["```".Length..];
        }

        if (response.EndsWith("```"))
        {
            response = response[..^3];
        }

        return response.Trim();
    }

    private sealed class OllamaResponse
    {
        public string Response { get; set; } = string.Empty;
    }
}