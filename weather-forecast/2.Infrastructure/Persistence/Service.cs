using Company.Product.Common.DependencyInjection;

namespace Company.Product.WeatherForecast.Persistence;

[RegisterType(Lifetime.Singleton, typeof(IService), typeof(int))]
public class Service : IService
{
    public string GetDateAsString()
    {
        return DateTime.Now.ToShortDateString();
    }
}
