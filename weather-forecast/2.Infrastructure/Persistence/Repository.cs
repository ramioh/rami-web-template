using Company.Product.WeatherForecast.Persistence.Contracts;

namespace Company.Product.WeatherForecast.Persistence;

public class Repository : IRepository
{
    public string GetHello()
    {
        return "hello!";
    }
}