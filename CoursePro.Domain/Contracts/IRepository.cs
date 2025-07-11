using CoursePro.Domain.Entities;

namespace CoursePro.Domain.Contracts
{
    public interface IRepository<TEntity, T2> where TEntity : IBaseEntity<T2>
    {
        Task<List<TEntity>> GetAsync();
        Task<TEntity> GetByIdAsync(T2 id);
        Task UpdateAsync(TEntity entity);
        Task AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
