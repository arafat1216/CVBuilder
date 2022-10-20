using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;

namespace CVBuilder.Infrastructure.Repositories
{
    public class PersonalDetailsUpdateRepository : BaseRepository<PersonalDetailsUpdateRequest>, IPersonalDetailsUpdateRepository
    {
        public PersonalDetailsUpdateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
