using CVBuilder.Application.Contracts.Persistence;
using MediatR;

namespace CVBuilder.Application.Features.Company.Commands.UpdateSubscriptionStatus
{
    public class UpdateSubscriptionStatusCommandHandler : IRequestHandler<UpdateSubscriptionStatusCommand>
    {
        private readonly ICompanyRepository repository;

        public UpdateSubscriptionStatusCommandHandler(ICompanyRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Unit> Handle(UpdateSubscriptionStatusCommand request, CancellationToken cancellationToken)
        {
            var company = await repository.GetCompanyByIdAsync(request.CompanyId);

            if (company == null)
                throw new Exceptions.NotFoundException(nameof(Company), request.CompanyId);

            company.SubscriptionStatus = request.SubscriptionStatus;

            await repository.UpdateAsync(company);

            return Unit.Value;
        }
    }
}
