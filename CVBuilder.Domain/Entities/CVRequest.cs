namespace CVBuilder.Domain.Entities
{
    public class CVRequest
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int RequestsAmount { get; set; }

        // Navigation Property
        public Guid CompanyId { get; set; }
    }
}
