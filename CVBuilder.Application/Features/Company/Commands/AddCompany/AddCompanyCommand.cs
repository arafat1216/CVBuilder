using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.Company.Commands.AddCompany
{
    public class AddCompanyCommand : IRequest<AddCompanyCommandResponse>
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
    }
}
