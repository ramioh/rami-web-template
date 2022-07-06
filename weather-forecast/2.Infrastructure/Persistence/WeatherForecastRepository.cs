using Company.Product.Common.ApiCore.DependencyInjection;
using Company.Product.WeatherForecast.Persistence.Contracts;

namespace Company.Product.WeatherForecast.Persistence;

[RegisterType(Lifetime.Singleton, typeof(IWeatherForecastRepository))]
public class WeatherForecastRepository : IWeatherForecastRepository
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public string GetSummary() => Summaries[Random.Shared.Next(Summaries.Length)];

    public int GetTemperature() => Random.Shared.Next(-20, 55);
}
