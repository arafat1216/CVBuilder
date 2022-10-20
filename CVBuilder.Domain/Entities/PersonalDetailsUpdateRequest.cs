namespace CVBuilder.Domain.Entities
{
    public class PersonalDetailsUpdateRequest
    {
        
        public int Id { get; set; }

        public string? FullName { get; set; }

        public Guid EmployeeId { get; set; }

        public string? PhoneNo { get; set; }
        
        public string? Address { get; set; }

        // Navigation Property

        public int RequestId { get; set; }

    }
}
