namespace CVBuilder.Domain.Entities
{
    public class CompanyDetails
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }

        // Navigation Property
        public Guid CompanyId { get; set; }

    }
}
