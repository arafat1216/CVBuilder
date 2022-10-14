namespace CVBuilder.Domain.Entities
{
    public class Degree
    {
        public int DegreeId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; } = "CSE";
        public string Institute { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation Property
        public Guid EmployeeId { get; set; }
    }
}
