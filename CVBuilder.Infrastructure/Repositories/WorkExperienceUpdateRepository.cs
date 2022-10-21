using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class WorkExperienceUpdateRepository : BaseRepository<WorkExperienceUpdateRequest>, IWorkExperienceUpdateRepository
    {
        public WorkExperienceUpdateRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<WorkExperienceUpdateRequest?> GetWorkExperienceUpdateRequestByIdAsync(int requestId)
        {
            return await dbSet.Where(w => w.RequestId.Equals(requestId)).FirstOrDefaultAsync();
        }
    }
}
