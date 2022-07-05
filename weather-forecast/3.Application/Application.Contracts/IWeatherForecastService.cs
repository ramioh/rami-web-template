namespace Company.Product.WeatherForecast.Application.Contracts;

public interface IWeatherForecastService
{
    string GetSummary();
    int GetTemperature();
}
