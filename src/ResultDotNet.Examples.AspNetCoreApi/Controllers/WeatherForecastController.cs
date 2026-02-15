using Microsoft.AspNetCore.Mvc;

namespace ResultDotNet.Examples.AspNetCoreApi.Controllers;

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
            .Bind(MapToWeatherForecastDto)
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

    private static Result<IEnumerable<WeatherForecastDto>, ProblemDetails> MapToWeatherForecastDto(IEnumerable<int> days) 
        => Result<IEnumerable<WeatherForecastDto>, ProblemDetails>.FromValue(
            days.Select(index => new WeatherForecastDto
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }));
}