using System.Collections.Concurrent;
using Dal.Interfaces;
using Domain;

namespace Dal;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ConcurrentDictionary<Type, object> _repsDictionary;
    
    public UnitOfWork()
    {
        _repsDictionary = new ConcurrentDictionary<Type, object>();
    }
    
    public IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class, IBaseEntity
    {
        if (_repsDictionary.TryGetValue(typeof(TEntity), out object repository))
            return (IRepository<TEntity>)repository;
        repository = new Repository<TEntity>();
        _repsDictionary.TryAdd(typeof(TEntity), repository);
        return (IRepository<TEntity>)repository;
    }
    
    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
        }

        _disposed = true;
    }

    ~UnitOfWork() => Dispose(false);
}