namespace CVBuilder.Domain.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }

        // Naviagtion Property
        public Guid EmployeeId { get; set; }
    }
}
