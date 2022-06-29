using Company.Product.Common.DependencyInjection;
using Company.Product.WeatherForecast.Persistence.Contracts;

namespace Company.Product.WeatherForecast.Persistence;

[RegisterType(Lifetime.Singleton, typeof(IRepository))]
public class Repository : IRepository
{
    public string GetHello()
    {
        return "hello!";
    }
}