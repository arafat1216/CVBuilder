using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IWorkExperienceRepository : IAsyncRepository<WorkExperience>
    {
        Task<IReadOnlyList<WorkExperience>> ListAllAsync(Guid employeeId);
        Task<WorkExperience?> GetWorkExperienceByIdAsync(Guid employeeId, int id);
        Task<bool> ExistsAsync(Guid employeeId, int id);
    }
}
