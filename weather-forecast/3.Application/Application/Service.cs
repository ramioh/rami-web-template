using Company.Product.Common.DependencyInjection;
using Company.Product.WeatherForecast.Application.Contracts;
using Company.Product.WeatherForecast.Persistence.Contracts;

namespace Company.Product.WeatherForecast.Application;

[RegisterType(Lifetime.Scoped, typeof(IService))]
public class Service : IService
{
    private readonly IRepository _repository;
    
    public Service(IRepository repository)
    {
        _repository = repository;
    }

    public string SayHello()
    {
        return _repository.GetHello();
    }
}