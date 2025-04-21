using System.Reflection;

namespace Manager_Device_Service.Providers
{
    public static class DependencyInjectionProvider
    {
        public static IServiceCollection AddDependencyInjectionProvider(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var serviceProjectNamespace = $"{Assembly.GetCallingAssembly().GetName().Name}.Services";
            //var serviceProjectNamespace = $"{Assembly.GetExecutingAssembly().GetName().Name}.Services";


            var serviceTypes = assembly.GetTypes()
                .Where(type => type.Namespace == serviceProjectNamespace && !type.IsAbstract && !type.IsInterface);

            foreach (var serviceType in serviceTypes)
            {
                services.AddScoped(serviceType);
            }

            return services;
        }
    }
}
