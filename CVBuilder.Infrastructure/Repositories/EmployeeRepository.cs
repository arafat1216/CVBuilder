using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Models.Pagination;
using CVBuilder.Application.ViewModels.Company;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Employee> dbset;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbset = context.Set<Employee>();
        }
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await dbset.AddAsync(employee);

            await context.SaveChangesAsync();

            return employee;
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            dbset.Remove(employee);

            await context.SaveChangesAsync();
        }

        public async Task<bool> EmployeeExistsAsync(Guid employeeId)
        {
            return await dbset.AnyAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<(List<Employee>, PaginationMetaData)> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            var collection = dbset as IQueryable<Employee>;

            collection = collection.Where(e => !e.IsDeleted);

            var totalItems = await collection.CountAsync();

            var paginationMetaData = new PaginationMetaData(totalItems, pageNumber, pageSize);

            var collectionToReturn = await collection
                .OrderBy(e => e.FullName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetaData);

        }

        public async Task<(List<Employee>, PaginationMetaData)> GetAllEmployeesCVAsync(string? searchBySkill, string? searchByDegree, string? searchByProject, int pageNumber, int pageSize)
        {
            var collection = dbset as IQueryable<Employee>;
            collection = collection
                .Include(e => e.Skills.Where(s => !s.IsDeleted))
                .Include(e => e.Degrees.Where(d => !d.IsDeleted))
                .Include(e => e.WorkExperiences.Where(w => !w.IsDeleted))
                .Include(e => e.Projects.Where(p => !p.IsDeleted))
                .Where(e => !e.IsDeleted);

            if (!string.IsNullOrEmpty(searchBySkill))
            {
                collection = ApplySearchBySkillFilter(searchBySkill, collection);

            }

            if (!string.IsNullOrEmpty(searchByDegree))
            {
                collection = ApplySearchByDegreeFilter(searchByDegree, collection);
            }

            if (!string.IsNullOrEmpty(searchByProject))
            {
                collection = ApplySearchByProjectFilter(searchByProject, collection);
            }

            var totalItems = await collection.CountAsync();

            var paginationMetaData = new PaginationMetaData(totalItems, pageNumber, pageSize);

            var collectionToReturn = await collection
                .OrderBy(e => e.FullName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetaData);
        }

        private IQueryable<Employee> ApplySearchByProjectFilter(string searchByProject, IQueryable<Employee> collection)
        {
            searchByProject = searchByProject.Trim();

            return collection.Where(e => e.Projects.Any(p => p.ProjectDetails.Name.Equals(searchByProject) && !p.IsDeleted));
        }

        private IQueryable<Employee> ApplySearchByDegreeFilter(string searchByDegree, IQueryable<Employee> collection)
        {
            searchByDegree = searchByDegree.Trim();

            return collection.Where(e => e.Degrees.Any(d => d.DegreeDetails.Subject.Equals(searchByDegree) && !d.IsDeleted));
        }

        private IQueryable<Employee> ApplySearchBySkillFilter(string searchBySkill, IQueryable<Employee> collection)
        {
            searchBySkill = searchBySkill.Trim();

            return collection.Where(e => e.Skills.Any(s => s.SkillDetails.Name == searchBySkill && !s.IsDeleted));
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            return await dbset.Where(e => e.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(Guid employeeId)
        {
            return await dbset
                .Include(e => e.Skills.Where(s => !s.IsDeleted))
                .Include(e => e.Degrees.Where(d => !d.IsDeleted))
                .Include(e => e.WorkExperiences.Where(w => !w.IsDeleted))
                .Include(e => e.Projects.Where(p => !p.IsDeleted))
                .Where(e => e.EmployeeId == employeeId).FirstOrDefaultAsync();
        }

        public async Task<Employee?> GetEmployeeDetailsAsync(Guid employeeId)
        {
            return await dbset.Where(e => e.EmployeeId == employeeId).FirstOrDefaultAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            dbset.Update(employee);

            await context.SaveChangesAsync();
        }


        public async Task UpdateEmployeePasswordAsync(Employee employee)
        {
            dbset.Update(employee);

            await context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllEmployeesCVAsync(CVRequestViewModel cVRequestViewModel)
        {
            var collection = dbset as IQueryable<Employee>;
            collection = collection
                .Include(e => e.Skills.Where(s => !s.IsDeleted))
                .Include(e => e.Degrees.Where(d => !d.IsDeleted))
                .Include(e => e.WorkExperiences.Where(w => !w.IsDeleted))
                .Include(e => e.Projects.Where(p => !p.IsDeleted))
                .Where(e => !e.IsDeleted);

            if (!string.IsNullOrEmpty(cVRequestViewModel.SearchBySkill))
            {
                collection = ApplySearchBySkillFilter(cVRequestViewModel.SearchBySkill, collection);

            }

            if (!string.IsNullOrEmpty(cVRequestViewModel.searchByDegree))
            {
                collection = ApplySearchByDegreeFilter(cVRequestViewModel.searchByDegree, collection);
            }

            if (!string.IsNullOrEmpty(cVRequestViewModel.searchByProject))
            {
                collection = ApplySearchByProjectFilter(cVRequestViewModel.searchByProject, collection);
            }

            var collectionToReturn = await collection
                .OrderBy(e => e.FullName)
                .ToListAsync();

            return collectionToReturn;
        }
    }
}
