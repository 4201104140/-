using AutoMapper;
using IdentityServer.Models;

namespace IdentityServer.EntityFramework.Mappers;

/// <summary>
/// Extension methods to map to/from entity/model for persisted grants.
/// </summary>
public static class PersistedGrantMappers
{
    static PersistedGrantMappers()
    {
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<PersistedGrantMapperProfile>())
            .CreateMapper();
    }

    internal static IMapper Mapper { get; }

    /// <summary>
    /// Maps an entity to a model.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    public static PersistedGrant ToModel(this Entities.PersistedGrant entity)
    {
        return entity == null ? null : Mapper.Map<PersistedGrant>(entity);
    }

    public static Entities.PersistedGrant ToEntity(this PersistedGrant model)
    {
        return model == null ? null : Mapper.Map<Entities.PersistedGrant>(model);
    }

    public static void UpdateEntity(this PersistedGrant model, Entities.PersistedGrant entity)
    {
        Mapper.Map(model, entity);
    }
}
