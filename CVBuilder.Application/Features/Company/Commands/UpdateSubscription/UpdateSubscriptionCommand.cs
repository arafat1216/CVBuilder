using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.Company.Commands.UpdateSubscription
{
    public class UpdateSubscriptionCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
    }
}
