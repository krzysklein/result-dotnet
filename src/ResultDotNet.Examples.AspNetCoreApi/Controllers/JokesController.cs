using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace ResultDotNet.Examples.AspNetCoreApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JokesController(
    IHttpClientFactory httpClientFactory)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken) 
        => await SampleDelayAsync(cancellationToken)
            .BindAsync(() => GetJokesFromApi(cancellationToken, httpClientFactory))
            .ToActionResultAsync();

    private static async Task<Result<ProblemDetails>> SampleDelayAsync(CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(5000, cancellationToken);
            return Result<ProblemDetails>.Success();
        }
        catch (OperationCanceledException)
        {
            return Result<ProblemDetails>.FromError(new ProblemDetails
            {
                Status = StatusCodes.Status499ClientClosedRequest,
                Title = "Request was cancelled."
            });
        }
    }

    private static async Task<Result<List<JokeDto>, ProblemDetails>> GetJokesFromApi(CancellationToken cancellationToken, IHttpClientFactory httpClientFactory)
    {
        using var httpClient = httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri("https://api.sampleapis.com");

        var result = await httpClient.GetFromJsonAsync<List<JokeDto>>("jokes/goodJokes", cancellationToken);
        return result!;
    }

    public class JokeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("setup")]
        public string? Setup { get; set; }

        [JsonPropertyName("punchline")]
        public string? Punchline { get; set; }
    }
}