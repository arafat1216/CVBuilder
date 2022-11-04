using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using MediatR;

namespace CVBuilder.Application.Features.Company.Commands.UpdateCompany
{
    public class UpdateCompanyDetailsCommandHandler : IRequestHandler<UpdateCompanyDetailsCommand>
    {
        private readonly ICompanyDetailsRepository companyDetailsRepository;
        private readonly IMapper mapper;

        public UpdateCompanyDetailsCommandHandler(ICompanyDetailsRepository companyDetailsRepository, IMapper mapper)
        {
            this.companyDetailsRepository = companyDetailsRepository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCompanyDetailsCommand request, CancellationToken cancellationToken)
        {
            var company = await companyDetailsRepository.GetCompanyDetailsById(request.CompanyId);

            if (company == null)
                throw new Exceptions.NotFoundException(nameof(Company), request.CompanyId);

            mapper.Map(request, company);

            await companyDetailsRepository.UpdateAsync(company);

            return Unit.Value;
        }
    }
}
