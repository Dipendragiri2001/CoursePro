using CoursePro.Domain.Contracts;
using CoursePro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoursePro.Infrastructure
{
    internal class Repository<TEntity, T2> : IRepository<TEntity, T2>
    where TEntity : class, IBaseEntity<T2>
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(T2 id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id!.Equals(id));
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
