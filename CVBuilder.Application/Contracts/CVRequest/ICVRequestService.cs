using CVBuilder.Application.Dtos.Employee;
using CVBuilder.Application.ViewModels.Company;

namespace CVBuilder.Application.Contracts.CVRequest
{
    public interface ICVRequestService
    {
        Task<List<EmployeeDetailsDto>> RequestCV(CVRequestViewModel cvRequestViewModel);
    }
}
