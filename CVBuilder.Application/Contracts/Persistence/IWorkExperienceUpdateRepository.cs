using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IWorkExperienceUpdateRepository : IAsyncRepository<WorkExperienceUpdateRequest>
    {
        Task<WorkExperienceUpdateRequest?> GetWorkExperienceUpdateRequestByIdAsync(int requestId);
    }
}
