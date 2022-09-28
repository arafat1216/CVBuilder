﻿using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class SkillRepository : BaseRepository<Skill>, ISkillRepository
    {
        public SkillRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAsync(Guid employeeId, int id)
        {
            return await context.Skills.AnyAsync(e => e.EmployeeId == employeeId && e.SkillId == id);
        }

        public async Task<Skill?> GetSkillByIdAsync(Guid employeeId, int id)
        {
            return await context.Skills.Where(e => e.EmployeeId == employeeId && e.SkillId == id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Skill>> ListAllAsync(Guid employeeId)
        {
            return await context.Skills.Where(e => e.EmployeeId == employeeId).ToListAsync();
        }
    }
}
