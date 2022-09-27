namespace CVBuilder.Domain.Entities
{
    public class Degree
    {
        public int DegreeId { get; set; }
        public string Name { get; set; }
        public string Institute { get; set; }

        // Navigation Property
        public Employee? Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
