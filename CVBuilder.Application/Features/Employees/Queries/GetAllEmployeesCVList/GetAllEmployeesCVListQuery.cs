using CVBuilder.Application.Dtos.Employee;
using CVBuilder.Application.Models.Pagination;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Queries.GetAllEmployeesCVList
{
    public class GetAllEmployeesCVListQuery : IRequest<(List<EmployeeDetailsDto>, PaginationMetaData)>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public RelatedData? RelatedData { get; set; }
        public string? SearchBySkill { get; set; }
        public string? SearchByDegree { get; set; }
        public string? SearchByProject { get; set; }
    }
}
