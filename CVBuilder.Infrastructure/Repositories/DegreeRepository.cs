using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class DegreeRepository : BaseRepository<Degree>, IDegreeRepository
    {
        public DegreeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAsync(Guid employeeId, int id)
        {
            return await context.Degrees.AnyAsync(e => e.EmployeeId == employeeId && e.DegreeId == id);
        }

        public async Task<Degree?> GetDegreeByIdAsync(Guid employeeId, int id)
        {
            return await context.Degrees.Where(e => e.EmployeeId == employeeId && e.DegreeId == id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Degree>> ListAllAsync(Guid employeeId)
        {
            return await context.Degrees.Where(e => e.EmployeeId == employeeId).ToListAsync();
        }
    }
}
