using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.Company;
using MediatR;

namespace CVBuilder.Application.Features.Company.Queries.GetCompaniesList
{
    public class GetCompaniesListQueryHandler : IRequestHandler<GetCompaniesListQuery, List<CompaniesListDto>>
    {
        private readonly ICompanyRepository repository;
        private readonly IMapper mapper;

        public GetCompaniesListQueryHandler(ICompanyRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<CompaniesListDto>> Handle(GetCompaniesListQuery request, CancellationToken cancellationToken)
        {
            var companiesList = await repository.GetAllCompanies();

            return mapper.Map<List<CompaniesListDto>>(companiesList);
        }
    }
}
