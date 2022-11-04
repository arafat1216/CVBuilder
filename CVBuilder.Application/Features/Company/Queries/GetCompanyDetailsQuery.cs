using CVBuilder.Application.Dtos.Company;
using MediatR;

namespace CVBuilder.Application.Features.Company.Queries
{
    public class GetCompanyDetailsQuery : IRequest<CompanyDetailsDto>
    {
        public Guid CompanyId { get; set; }
    }
}
