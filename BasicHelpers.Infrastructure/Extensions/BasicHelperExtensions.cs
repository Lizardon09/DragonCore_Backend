using BasicHelpers.Infrastructure.Services.Interfaces;
using BasicHelpers.Infrastructure.Services.Models;
using BasicHelpers.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BasicHelpers.Infrastructure.Extensions
{
    public static class BasicHelperExtensions
    {
        // Procrastinated code conditions to be refactored for selective dependency injection of services from library
        public static void ConfigureBasicHelpers(this IServiceCollection services, BasicHelperSettings settings)
        {
            foreach (IBasicHelperService? helperService in settings.BasicHelperServices)
            {
                if (helperService != null)
                {
                    Type serviceImplimentation = helperService.GetType();
                    Type? serviceInterface = helperService.GetMainInterface();

                    if(serviceInterface != null)
                    {
                        services.AddScoped(serviceInterface, serviceImplimentation);
                        var dependencyTree = helperService.GetDependencies();
                        if(dependencyTree != null)
                        {
                            foreach (Dependency? dependencyService in dependencyTree)
                            {
                                if (dependencyService != null)
                                {
                                    serviceImplimentation = dependencyService.Implimentation;
                                    serviceInterface = dependencyService.Interface;
                                    if (serviceInterface != null)
                                    {
                                        services.TryAddScoped(serviceInterface, serviceImplimentation);

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
