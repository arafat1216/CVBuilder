using CVBuilder.Application.Contracts.Persistence;
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

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await dbset.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            return await dbset.Where(e => e.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(Guid employeeId)
        {
            return await dbset
                .Include(e => e.Skills)
                .Include(e=> e.Degrees)
                .Include(e => e.WorkExperiences)
                .Include(e => e.Projects)
                .Where(e => e.EmployeeId == employeeId).FirstOrDefaultAsync();
        }

        
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var employeeToBeUpdated = await GetEmployeeByIdAsync(employee.EmployeeId);
            employeeToBeUpdated.FullName = employee.FullName;
            employeeToBeUpdated.Address = employee.Address;
            employeeToBeUpdated.PhoneNo = employee.PhoneNo;
            employeeToBeUpdated.Role = employee.Role;
            employeeToBeUpdated.Email = employee.Email;

            dbset.Update(employeeToBeUpdated);

            await context.SaveChangesAsync();
        }

        public async Task UpdateEmployeePasswordAsync(Employee employee)
        {
            dbset.Update(employee);

            await context.SaveChangesAsync();
        }
    }
}
