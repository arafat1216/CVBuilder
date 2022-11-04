using MediatR;

namespace CVBuilder.Application.Features.Company.Commands.UpdateCompany
{
    public class UpdateCompanyDetailsCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
    }
}
