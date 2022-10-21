using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class PersonalDetailsUpdateRepository : BaseRepository<PersonalDetailsUpdateRequest>, IPersonalDetailsUpdateRepository
    {
        public PersonalDetailsUpdateRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PersonalDetailsUpdateRequest?> GetPersonalDetailsUpdateRequestByIdAsync(int requestId)
        {
            return await dbSet.Where(p => p.RequestId.Equals(requestId)).FirstOrDefaultAsync();
        }
    }
}
