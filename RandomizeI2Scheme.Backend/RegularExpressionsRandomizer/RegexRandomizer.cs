using Fare;
using Microsoft.Extensions.DependencyInjection;

namespace RegularExpressionsRandomizer;

public class RegexRandomizer : IRegexRandomizer
{
    public string GetData(string regexString)
    {
        Xeger xeger = new Xeger(regexString, new Random());
        return xeger.Generate();
    }
}

public static class DependencyInjection
{
    public static IServiceCollection AddRegexRandomizer(this IServiceCollection services)
    {
        services.AddTransient<IRegexRandomizer, RegexRandomizer>();

        return services;
    }
}
public interface IRegexRandomizer
{
   string GetData(string regexString);
}
