using Microsoft.AspNetCore.Mvc;

namespace ResultDotNet.Examples.AspNetCoreApi.Controllers;

/// <summary>
/// This is a sample controller that demonstrates how to use the Result pattern 
/// to handle input validation and error handling in an ASP.NET Core API. 
/// </summary>
[ApiController]
[Route("[controller]")]
public class WeatherForecastController 
    : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [HttpGet]
    public IActionResult Get(int numDays = 5) 
        => ValidateInput(numDays)
            .Bind(GenerateDayRange)
            .Map(MapToWeatherForecastDto)
            .ToActionResult();

    private static Result<int, ProblemDetails> ValidateInput(int numDays)
    {
        if (numDays < 1)
        {
            return new ProblemDetails
            {
                Title = "Invalid number of days",
                Detail = "The number of days must be at least 1.",
                Status = StatusCodes.Status400BadRequest
            };
        }

        return numDays;
    }

    private static Result<IEnumerable<int>, ProblemDetails> GenerateDayRange(int numDays) 
        => Result<IEnumerable<int>, ProblemDetails>.FromValue(
            Enumerable.Range(1, numDays));

    private static IEnumerable<WeatherForecastDto> MapToWeatherForecastDto(IEnumerable<int> days) 
        => days.Select(index => new WeatherForecastDto
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
}