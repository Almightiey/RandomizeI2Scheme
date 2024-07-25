using I2Scheme.Application.Interfaces;
using I2Scheme.Application.SchemeDataManagment;
using Microsoft.Extensions.DependencyInjection;

namespace I2Scheme.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISchemeDataManager, SchemeToDb>();
        services.AddScoped<IRandomizeManager, RandomizeManager>();

        return services;
    }
}

