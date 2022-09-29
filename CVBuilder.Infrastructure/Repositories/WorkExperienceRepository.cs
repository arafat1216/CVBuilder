using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class WorkExperienceRepository : BaseRepository<WorkExperience>, IWorkExperienceRepository
    {
        public WorkExperienceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAsync(Guid employeeId, int id)
        {
            return await context.WorkExperiences.AnyAsync(e => e.EmployeeId == employeeId && e.WorkExperienceId == id);
        }

        public async Task<WorkExperience?> GetWorkExperienceByIdAsync(Guid employeeId, int id)
        {
            return await context.WorkExperiences
                .Where(e => e.EmployeeId == employeeId && e.WorkExperienceId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<WorkExperience>> ListAllAsync(Guid employeeId)
        {
            return await context.WorkExperiences.Where(e => e.EmployeeId == employeeId).ToListAsync();
        }
    }
}
