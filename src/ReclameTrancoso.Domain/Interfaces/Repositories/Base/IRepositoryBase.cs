using Domain.Models;

namespace Domain.Interfaces;

public interface IRepositoryBase<TEntity>  where TEntity : BaseEntity
{
    ValueTask<TEntity> SaveAsync(TEntity entity);
    ValueTask<TEntity> GetByIdAsync(long? id);
    ValueTask<IEnumerable<TEntity>> GetAsync();
    Task DeleteAsync(long? id);         
    ValueTask<bool> ExistsByIdAsync(long? id);
}