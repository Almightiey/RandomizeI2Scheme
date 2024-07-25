using I2Scheme.Persistece.Models;

namespace I2Scheme.Application.Interfaces;

public interface ISchemeDataManager
{
    Task<ICollection<I2scheme>> GetAllSchemesAsync(CancellationToken token);
    Task<I2scheme> GetSchemeByIdAsync(int id, CancellationToken token);
    Task<I2scheme> GetSchemeByName(string name, CancellationToken token);
    Task<I2scheme> CreateSchemeAsync(I2scheme model, CancellationToken token);
    Task DeleteSchemeAsync(int id, CancellationToken token);
    Task UpdateSchemeAsync(int id, I2scheme model, CancellationToken token);
}