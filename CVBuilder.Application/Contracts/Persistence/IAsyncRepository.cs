namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> ListAllAsync(Guid employeeId);
        Task<T> GetByIdAsync(Guid employeeId, int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid employeeId, int id);
        Task<bool> ExistsAsync(Guid employeeId, int id);

    }
}
