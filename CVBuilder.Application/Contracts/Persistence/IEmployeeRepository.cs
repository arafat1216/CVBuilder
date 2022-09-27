using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(Guid employeeId);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Guid employeeId);
        Task<bool> EmployeeExistsAsync(Guid employeeId);
    }
}
