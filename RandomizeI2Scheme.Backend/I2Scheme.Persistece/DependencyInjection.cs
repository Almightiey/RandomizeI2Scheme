using I2Scheme.Persistece.DI.Interfaces;
using I2Scheme.Persistece.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace I2Scheme.Persistece;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Triton_SchemasContext");
        services.AddDbContext<TritonSchemasContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<I2SchemeDbContext>(provider =>
            provider.GetService<TritonSchemasContext>());

        
        return services;
    }
}

