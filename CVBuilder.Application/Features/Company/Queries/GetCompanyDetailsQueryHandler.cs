using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.Company;
using MediatR;

namespace CVBuilder.Application.Features.Company.Queries
{
    public class GetCompanyDetailsQueryHandler : IRequestHandler<GetCompanyDetailsQuery, CompanyDetailsDto>
    {
        private readonly ICompanyRepository repository;
        private readonly IMapper mapper;

        public GetCompanyDetailsQueryHandler(ICompanyRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CompanyDetailsDto> Handle(GetCompanyDetailsQuery request, CancellationToken cancellationToken)
        {
            var companyDetails = await repository.GetCompanyByIdAsync(request.CompanyId);

            return mapper.Map<CompanyDetailsDto>(companyDetails);
        }
    }
}
