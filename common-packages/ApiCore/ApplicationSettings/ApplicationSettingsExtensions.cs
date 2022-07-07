using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Product.Common.ApiCore.ApplicationSettings;

public static class ApplicationSettingsExtensions
{
    public static IServiceCollection ConfigureApplicationSettings(this IServiceCollection services, ConfigurationManager configuration)
    {
        var dd = services.AddOptions();
        return services;
        //
        // services.ConfigureOptions(.Configure() . Configure<PositionOptions>(
        //     builder.Configuration.GetSection(PositionOptions.Position));
        //
        // configuration.GetSection()
        //
        // configuration.GetSection()
        // var prefix = GetAssemblyNamePrefix();
        // var loadedAssemblies = LoadAssembliesByPrefix(prefix);
        // var typeRegistrations = GetTypeRegistrations(loadedAssemblies);
        //
        // var serviceDescriptors = typeRegistrations
        //     .SelectMany(x => x.Registration.Types, (x, y) => new ServiceDescriptor(y, x.Implementation, MapServiceLifetime(x.Registration.Lifetime)));
        //
        // return services.Add(serviceDescriptors);
    }
}