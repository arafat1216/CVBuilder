namespace CVBuilder.Application.Dtos.Company
{
    public class CompanyDetailsDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string UserName { get; set; }
        public string SubscriptionType { get; set; }
    }
}
