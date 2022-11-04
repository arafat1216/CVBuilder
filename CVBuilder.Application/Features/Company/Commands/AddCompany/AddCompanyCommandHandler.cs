using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using MediatR;
using CVBuilder.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Company.Commands.AddCompany
{
    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, AddCompanyCommandResponse>
    {
        private readonly ICompanyRepository repository;
        private readonly IMapper mapper;

        public AddCompanyCommandHandler(ICompanyRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            
        }

        public async Task<AddCompanyCommandResponse> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = mapper.Map<Domain.Entities.Company>(request);

            company.Password = GetHashedPassword(request.Password);

            var companyDetails = mapper.Map<CompanyDetails>(request);

            company.CompanyDetails = companyDetails;

            var response = await repository.AddAsync(company);


            return mapper.Map<AddCompanyCommandResponse>(response);
        }

        private string GetHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
