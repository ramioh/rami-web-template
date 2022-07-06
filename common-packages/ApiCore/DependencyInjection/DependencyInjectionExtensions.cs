using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Company.Product.Common.ApiCore.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection ConfigureContainerDynamically(this IServiceCollection services)
    {
        var prefix = GetAssemblyNamePrefix();
        var loadedAssemblies = LoadAssembliesByPrefix(prefix);
        var typeRegistrations = GetTypeRegistrations(loadedAssemblies);

        var serviceDescriptors = typeRegistrations
            .SelectMany(x => x.Registration.Types, (x, y) => new ServiceDescriptor(y, x.Implementation, MapServiceLifetime(x.Registration.Lifetime)));

        return services.Add(serviceDescriptors);
    }

    private static string GetAssemblyNamePrefix()
    {
        const string errorMessage = "All application assemblies must start with {Company}.{Product}.{Service}.";
        var assemblyName = Assembly.GetEntryAssembly()!.FullName;
        var assemblyNameParts = assemblyName!.Split('.');

        if (assemblyNameParts.Length < 3)
        {
            throw new ArgumentOutOfRangeException(assemblyName, errorMessage);
        }

        var companyName = assemblyNameParts[0];
        var productName = assemblyNameParts[1];
        var serviceName = assemblyNameParts[2];

        if (string.IsNullOrWhiteSpace(companyName)
            || string.IsNullOrWhiteSpace(productName)
            || string.IsNullOrWhiteSpace(serviceName))
        {
            throw new ArgumentOutOfRangeException(assemblyName, errorMessage);
        }

        return $"{companyName}.{productName}.{serviceName}";
    }

    private static IReadOnlyCollection<Assembly> LoadAssembliesByPrefix(string prefix)
        => Directory
            .EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly)
            .Where(x => Path.GetFileName(x).StartsWith(prefix))
            .Select(Assembly.LoadFrom)
            .ToList();

    private static IReadOnlyCollection<TypeRegistrationInfo> GetTypeRegistrations(IEnumerable<Assembly> assemblies)
        => assemblies
            .SelectMany(x => x.DefinedTypes)
            .Aggregate(new List<TypeRegistrationInfo>(), (x, y) =>
            {
                var registerTypeAttribute = y.GetCustomAttributes<RegisterTypeAttribute>(inherit: false).FirstOrDefault();

                if (registerTypeAttribute is not null)
                {
                    x.Add(new TypeRegistrationInfo(y, registerTypeAttribute));
                }

                return x;
            });

    private static ServiceLifetime MapServiceLifetime(Lifetime lifetime)
        => lifetime switch
        {
            Lifetime.Singleton => ServiceLifetime.Singleton,
            Lifetime.Scoped => ServiceLifetime.Scoped,
            Lifetime.Transient => ServiceLifetime.Transient,
            _ => throw new ArgumentOutOfRangeException(nameof(lifetime)),
        };

    private record TypeRegistrationInfo(Type Implementation, RegisterTypeAttribute Registration);
}