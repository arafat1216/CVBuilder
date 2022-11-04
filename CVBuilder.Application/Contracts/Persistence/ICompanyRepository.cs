using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface ICompanyRepository : IAsyncRepository<Company>
    {
        Task<Company?> GetCompanyByIdAsync(Guid id);
        Task<Company?> GetCompanyByNameAsync(string name);
    }
}
