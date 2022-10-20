using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IProjectUpdateRepository : IAsyncRepository<ProjectUpdateRequest>
    {
    }
}
