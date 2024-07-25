using Microsoft.Extensions.DependencyInjection;

namespace FullnameRandomizer;

public static class DependencyInjection
{

    public static IServiceCollection AddRandomizeName(this IServiceCollection services)
    {
        services.AddTransient<IRandomizeName, RandFullname>();

        return services;
    }
}


