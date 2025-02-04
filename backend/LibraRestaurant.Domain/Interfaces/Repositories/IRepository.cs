using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> GetAllNoTracking();

    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity?> GetByIdAsync(int id);

    void Update(TEntity entity);

    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsAsync(int id);
    public void Remove(TEntity entity, bool hardDelete = false);
    void RemoveRange(IEnumerable<TEntity> entities, bool hardDelete = false);
}