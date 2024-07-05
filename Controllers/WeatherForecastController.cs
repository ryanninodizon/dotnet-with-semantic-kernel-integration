using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;

namespace dotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get(Kernel kernel)
    {
        var temperature = Random.Shared.Next(-20, 55);
        var result =new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = temperature,
            Summary = await kernel.InvokePromptAsync<string>($"Say something about this temperature {temperature} C")
        };
        return Ok(result);
    }
    [HttpPost(Name = "AskMe")]
    public async Task<IActionResult> AskMe(Kernel kernel, string question)
    {
        var result = await kernel.InvokePromptAsync<string>(question);
        return Ok(result);
    }
}