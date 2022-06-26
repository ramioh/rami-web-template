using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Company.Product.Common.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection ConfigureContainerDynamically(this IServiceCollection services)
    {
        var prefix = GetAssemblyNamePrefix();
        var loadedAssemblies = LoadAssemblies(prefix);
        var typeRegistrations = GetTypeRegistrations(loadedAssemblies);

        var serviceDescriptors = typeRegistrations
            .Select(x => new ServiceDescriptor(x.ServiceType, x.ImplementationType, x.Lifetime));

        return services.Add(serviceDescriptors);
    }

    private static string GetAssemblyNamePrefix()
    {
        var assembly = Assembly.GetEntryAssembly();
        var assemblyNameParts = assembly!.FullName!.Split('.');
        var companyName = assemblyNameParts[0];
        var productName = assemblyNameParts[1];
        var serviceName = assemblyNameParts[2];
        return $"{companyName}.{productName}.{serviceName}";
    }

    private static IReadOnlyCollection<Assembly> LoadAssemblies(string prefix)
        => Directory
            .EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly)
            .Where(x => Path.GetFileName(x).StartsWith(prefix))
            .Select(Assembly.LoadFrom)
            .ToList();

    private static IEnumerable<(TypeInfo ImplementationType, Type ServiceType, ServiceLifetime Lifetime)>
        GetTypeRegistrations(IEnumerable<Assembly> assemblies)
    {
        var definedTypes = assemblies
            .SelectMany(x => x.DefinedTypes);

        var custom = definedTypes
            .Select(x => (ImplementationType: x, Registration: x.GetCustomAttributes<RegisterTypeAttribute>(inherit: false).FirstOrDefault()))
            .Where(x => x.Registration is not null);
                
                var manymany = custom
            .SelectMany(x => x.Registration.Types,
                (x, y) => (x.ImplementationType, y, MapServiceLifetime(x.Registration.Lifetime)));

                return manymany;
    }

    private static ServiceLifetime MapServiceLifetime(Lifetime lifetime)
        => lifetime switch
        {
            Lifetime.Singleton => ServiceLifetime.Singleton,
            Lifetime.Scoped => ServiceLifetime.Scoped,
            Lifetime.Transient => ServiceLifetime.Transient,
            _ => throw new ArgumentOutOfRangeException(nameof(lifetime)),
        };
}