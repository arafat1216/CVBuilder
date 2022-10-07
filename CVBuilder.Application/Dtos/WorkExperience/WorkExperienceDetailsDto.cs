namespace CVBuilder.Application.Dtos.WorkExperience
{
    public class WorkExperienceDetailsDto
    {
        public int WorkExperienceId { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
