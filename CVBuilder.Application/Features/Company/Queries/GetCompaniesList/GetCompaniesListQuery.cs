using CVBuilder.Application.Dtos.Company;
using MediatR;

namespace CVBuilder.Application.Features.Company.Queries.GetCompaniesList
{
    public class GetCompaniesListQuery : IRequest<List<CompaniesListDto>>
    {
    }
}
