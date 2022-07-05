namespace Company.Product.WeatherForecast.Api.ViewModels;

public record WeatherForecastResponse(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
