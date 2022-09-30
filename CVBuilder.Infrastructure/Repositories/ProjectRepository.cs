using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
           
        }

        public async Task<bool> ExistsAsync(Guid employeeId, int id)
        {
            return await context.Projects.AnyAsync(e => e.EmployeeId == employeeId && e.ProjectId == id);
        }

        public async Task<Project?> GetProjectByIdAsync(Guid employeeId, int id)
        {
            return await context.Projects.Where(e => e.EmployeeId == employeeId && e.ProjectId == id).FirstOrDefaultAsync();
        }

        
    }
}
