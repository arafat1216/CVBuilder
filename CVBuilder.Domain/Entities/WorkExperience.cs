namespace CVBuilder.Domain.Entities
{
    public class WorkExperience
    {
        public int WorkExperienceId { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation Property
        public Guid EmployeeId { get; set; }
    }
}
