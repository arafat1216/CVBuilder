using CVBuilder.Application.Models.Pagination;
using CVBuilder.Application.ViewModels.Company;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IEmployeeRepository
    {
        Task<(List<Employee>, PaginationMetaData)> GetAllEmployeesAsync(int pageNumber, int pageSize);
        Task<(List<Employee>, PaginationMetaData)> GetAllEmployeesCVAsync(List<RelatedData>
            relatedDataList ,string? searchBySkill, string? searchByDegree,
            string? searchByProject, int pageNumber, int pageSize);
        Task<List<Employee>> GetAllEmployeesCVAsync(CVRequestViewModel cVRequestViewModel);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee?> GetEmployeeByIdAsync(Guid employeeId);
        Task<Employee?> GetEmployeeByEmailAsync(string email);
        Task<Employee?> GetEmployeeDetailsAsync(Guid employeeId);
        Task UpdateEmployeeAsync(Employee employee);
        Task UpdateEmployeePasswordAsync(Employee employee);    
        Task DeleteEmployeeAsync(Employee employee);
        Task<bool> EmployeeExistsAsync(Guid employeeId);
    }
}
