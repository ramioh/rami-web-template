namespace Company.Product.WeatherForecast.Persistence.Contracts;

public interface IWeatherForecastRepository
{
    string GetSummary();
    int GetTemperature();
}