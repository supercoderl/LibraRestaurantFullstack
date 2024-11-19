using System;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public Task<bool> CommitAsync();
}