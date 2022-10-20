using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;

namespace CVBuilder.Infrastructure.Repositories
{
    public class WorkExperienceUpdateRepository : BaseRepository<WorkExperienceUpdateRequest>, IWorkExperienceUpdateRepository
    {
        public WorkExperienceUpdateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
