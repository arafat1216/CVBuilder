using System.Linq.Expressions;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T,bool>> predicate);
        
    }
}
