using CVBuilder.Domain.Enums;

namespace CVBuilder.Domain.Entities
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; } = Role.Company;
        public SubscriptionType SubscriptionType { get; set; }
        public DateTime SubscriptionPurchasedDate { get; set; } = DateTime.Now;
        public SubscriptionStatus SubscriptionStatus { get; set; } = SubscriptionStatus.Active;

        // Navigation Property
        public CompanyDetails? CompanyDetails { get; set; }
        public ICollection<CVRequest> CVRequests { get; set; } = new List<CVRequest>();
    }
}
