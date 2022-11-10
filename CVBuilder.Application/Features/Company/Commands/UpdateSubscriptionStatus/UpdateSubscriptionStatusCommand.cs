using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.Company.Commands.UpdateSubscriptionStatus
{
    public class UpdateSubscriptionStatusCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public SubscriptionStatus SubscriptionStatus { get; set; }
    }
}
