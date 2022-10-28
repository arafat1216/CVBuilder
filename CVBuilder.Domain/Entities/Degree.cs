using CVBuilder.Domain.ValueObjects;

namespace CVBuilder.Domain.Entities
{
    public class Degree
    {
        public int DegreeId { get; set; }
        public DegreeDetails DegreeDetails { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation Property
        public Guid EmployeeId { get; set; }
    }
}