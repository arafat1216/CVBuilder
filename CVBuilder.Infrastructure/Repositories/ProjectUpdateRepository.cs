using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;

namespace CVBuilder.Infrastructure.Repositories
{
    public class ProjectUpdateRepository : BaseRepository<ProjectUpdateRequest>, IProjectUpdateRepository
    {
        public ProjectUpdateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
