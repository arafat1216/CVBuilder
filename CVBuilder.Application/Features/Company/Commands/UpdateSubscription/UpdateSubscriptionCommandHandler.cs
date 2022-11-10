using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.Company.Commands.UpdateSubscription
{
    public class UpdateSubscriptionCommandHandler : IRequestHandler<UpdateSubscriptionCommand>
    {
        private readonly ICompanyRepository repository;

        public UpdateSubscriptionCommandHandler(ICompanyRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Unit> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var company = await repository.GetCompanyByIdAsync(request.CompanyId);

            if (company == null)
                throw new Exceptions.NotFoundException(nameof(Company), request.CompanyId);

            company.SubscriptionType = request.SubscriptionType;

            company.SubscriptionPurchasedDate = DateTime.Now;

            company.SubscriptionStatus = SubscriptionStatus.Active;

            await repository.UpdateAsync(company);

            return Unit.Value;
        }
    }
}
