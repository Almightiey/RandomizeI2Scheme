using I2Scheme.Application.Interfaces;
using I2Scheme.Persistece.DI.Interfaces;
using I2Scheme.Persistece.Models;
using Microsoft.EntityFrameworkCore;

namespace I2Scheme.Application.SchemeDataManagment;

public class SchemeToDb : ISchemeDataManager
{
    private readonly I2SchemeDbContext _dbContext;
    private readonly IRandomizeManager _randomizeManager;

    public SchemeToDb(I2SchemeDbContext dbContext, IRandomizeManager randomizeManager)
    {
        _dbContext = dbContext;
        _randomizeManager = randomizeManager;
    }

    public async Task<ICollection<I2scheme>> GetAllSchemesAsync(CancellationToken token)
    {
        var models = await _dbContext.I2schemes.ToListAsync(token);
        return models;
    }

    public async Task<I2scheme> GetSchemeByIdAsync(int id, CancellationToken token)
    {
        var entity = await FindI2scheme(id, token);

        var icons = _dbContext.IconInfos.Where(icon => icon.SchemeId.Equals(entity.Id)).ToList();

        foreach (var icon in icons)
        {
            var iconFrame = _dbContext.IconFrames.FirstOrDefault(frame => frame.IconInfoId.Equals(icon.Id));
            icon.IconFrame = iconFrame;
            var attributes = _dbContext.AtributeInfos.Where(atribute => atribute.IconId.Equals(icon.Id)).ToList();
            icon.AtributeInfos = attributes;
            icon.Scheme = entity;
        }

        var relationships = _dbContext.RelationshipInfos.Where(rel => rel.SchemeId.Equals(entity.Id)).ToList();

        foreach (var relationship in relationships)
        {
            relationship.LinkStyle =
                await _dbContext.LinkStyles.FirstOrDefaultAsync(link => link.RelationshipId.Equals(relationship.Id));
            var attributes = _dbContext.AtributeInfos.Where(atribute => atribute.RelationshipId.Equals(relationship.Id))
                .ToList();
            relationship.AtributeInfos = attributes;
            relationship.Scheme = entity;
        }

        entity.RelationshipInfos = relationships;
        entity.IconInfos = icons;


        return entity;
    }

    public async Task<I2scheme> GetSchemeByName(string name, CancellationToken token)
    {
        var entity = await FindI2scheme(name, token);

        return entity;
    }

    public async Task<I2scheme> CreateSchemeAsync(I2scheme model, CancellationToken token)
    {
        var scheme = _randomizeManager.FillData(model);

        await _dbContext.I2schemes.AddAsync(model, token);
        await _dbContext.SaveChangesAsync(token);

        return scheme;
    }

    public async Task DeleteSchemeAsync(int id, CancellationToken token)
    {
        var scheme = await FindI2scheme(id, token);

        _dbContext.I2schemes.Remove(scheme);

        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateSchemeAsync(int id, I2scheme model, CancellationToken token)
    {
        var entity = await GetSchemeByIdAsync(id, token);

        entity.IconInfos = model.IconInfos;
        entity.SchemeName = model.SchemeName;
        entity.RelationshipInfos = model.RelationshipInfos;

        await _dbContext.SaveChangesAsync(token);
    }

    private async Task<I2scheme> FindI2scheme(string name, CancellationToken token)
    {
        var entity = await _dbContext.I2schemes.FirstAsync(obj => obj.SchemeName == name, token);
        if (entity == null || entity.SchemeName != name) throw new NullReferenceException("Not Found exception");

        return entity;
    }

    private async Task<I2scheme> FindI2scheme(int id, CancellationToken token)
    {
        var entity = await _dbContext.I2schemes.FirstAsync(scheme => scheme.Id == id, token);
        if (entity == null || entity.Id != id)
            throw new Exception("Not Found exception");

        return entity;
    }
}