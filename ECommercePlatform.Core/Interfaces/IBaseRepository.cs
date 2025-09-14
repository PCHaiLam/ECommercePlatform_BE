using System.Linq.Expressions;
using ECommercePlatform.Core.Entities;

namespace ECommercePlatform.Core.Interfaces
{
    public interface IRepository<T> where T: BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(long id);
        Task<List<T>> GetWithConditionAsync(Expression<Func<T, bool>> predicate);
        Task InsertAsync(T entity);
        Task InsertMultipleAsync(IList<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateMultipleAsync(IList<T> entities);
        Task DeleteAsync(long id);
        Task DeleteMultipleAsync(IList<T> entities);
    }
}
