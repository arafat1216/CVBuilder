using CVBuilder.Domain.Enums;

namespace CVBuilder.Application.Features.Company.Commands.AddCompany
{
    public class AddCompanyCommandResponse
    {
        public Guid CompanyId { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string UserName { get; set; }
        public string SubscriptionType { get; set; }
    }
}
