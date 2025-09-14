using System.Linq.Expressions;
using ECommercePlatform.Core.Entities;
using ECommercePlatform.Core.Interfaces;
using ECommercePlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommercePlatform.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

            if (result == null)
            {
                throw new KeyNotFoundException($"{typeof(T).Name} with id={id} not found");
            }

            return result;
        }

        public async Task<List<T>> GetWithConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task InsertMultipleAsync(IList<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateMultipleAsync(IList<T> entities)
        {
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                throw new KeyNotFoundException($"{typeof(T).Name} with id={id} not found");
            }

            entity.Deleted = true;
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMultipleAsync(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.Deleted = true;
            }
            
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}


