using Domain;

namespace Dal.Interfaces;

public interface IUnitOfWork
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity;
}