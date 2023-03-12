using Domain;

namespace Dal.Interfaces;

public interface IRepository<TEntity> where TEntity: IBaseEntity
{
    public Task Add(TEntity entity);
    
    public Task AddRange(IEnumerable<TEntity> entities);

    public Task Delete(int id);
    
    public Task DeleteRange(IEnumerable<int> ids);

    public Task Update(TEntity entity);

    public Task<IEnumerable<TEntity>> GetAll();

    public Task<TEntity> GetById(int id);
}