namespace CVBuilder.Application.Dtos.ResourceRequests
{
    public class PersonalDetailsUpdateRequestDto
    {
        public int Id { get; set; }

        public Guid EmployeeId { get; set; }
        
        public string? FullName { get; set; }

        public string? PhoneNo { get; set; }

        public string? Address { get; set; }
    }
}
