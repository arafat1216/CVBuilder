using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IProjectRepository : IAsyncRepository<Project>
    {
        Task<Project?> GetProjectByIdAsync(Guid employeeId, int id);
        Task<bool> ExistsAsync(Guid employeeId, int id);
    }
}
