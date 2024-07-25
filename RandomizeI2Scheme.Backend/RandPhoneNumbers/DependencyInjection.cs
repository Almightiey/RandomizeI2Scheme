using Microsoft.Extensions.DependencyInjection;

namespace RandPhoneNumbers;

public static class DependencyInjection
{
    public static IServiceCollection AddRandPhoneNumbers(this IServiceCollection services)
    {
        services.AddTransient<IPhoneNumbersRandomizer, RandPhoneNumbers>();
        return services;
    }
}
