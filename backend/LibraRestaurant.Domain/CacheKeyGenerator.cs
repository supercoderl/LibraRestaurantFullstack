using System;
using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Domain;

public static class CacheKeyGenerator
{
    public static string GetEntityCacheKey<TEntity>(TEntity entity) where TEntity : Entity
    {
        return $"{typeof(TEntity)}-{entity.Id}";
    }

    public static string GetEntityCacheKey<TEntity>(Guid id) where TEntity : Entity
    {
        return $"{typeof(TEntity)}-{id}";
    }

    public static string GetEntityCacheKey<TEntity>(int id) where TEntity : Entity
    {
        return $"{typeof(TEntity)}-{id}";
    }
}