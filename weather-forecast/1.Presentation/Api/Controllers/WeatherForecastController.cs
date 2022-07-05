using Company.Product.WeatherForecast.Application.Contracts;
using Company.Product.WeatherForecast.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Company.Product.WeatherForecast.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _service;

    public WeatherForecastController(IWeatherForecastService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecastResponse> Get()
    {
        return Enumerable
            .Range(1, 5)
            .Select(index =>
                new WeatherForecastResponse(DateTime.Now.AddDays(index), _service.GetTemperature(), _service.GetSummary()));
    }
}
