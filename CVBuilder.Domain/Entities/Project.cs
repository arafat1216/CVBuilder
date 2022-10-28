using CVBuilder.Domain.ValueObjects;

namespace CVBuilder.Domain.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public ProjectDetails ProjectDetails { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Naviagtion Property
        public Guid EmployeeId { get; set; }
    }
}
