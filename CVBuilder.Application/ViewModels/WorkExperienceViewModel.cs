namespace CVBuilder.Application.ViewModels
{
    public class WorkExperienceViewModel
    {
        public int WorkExperienceId { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
