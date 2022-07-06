using Company.Product.Common.ApiCore.DependencyInjection;
using Company.Product.WeatherForecast.Application.Contracts;
using Company.Product.WeatherForecast.Persistence.Contracts;

namespace Company.Product.WeatherForecast.Application;

[RegisterType(Lifetime.Transient, typeof(IWeatherForecastService))]
public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastRepository _repository;
    
    public WeatherForecastService(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public string GetSummary() => _repository.GetSummary();
    public int GetTemperature() => _repository.GetTemperature();
}
