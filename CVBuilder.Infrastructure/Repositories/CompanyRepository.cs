using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Company?> GetCompanyByIdAsync(Guid id)
        {
            return await dbSet.Include(c => c.CompanyDetails).Where(c => c.CompanyId == id).FirstOrDefaultAsync();
        }

        public async Task<Company?> GetCompanyByNameAsync(string name)
        {
            return await dbSet.Where(c => c.UserName== name).FirstOrDefaultAsync(); 
        }
    }
}
