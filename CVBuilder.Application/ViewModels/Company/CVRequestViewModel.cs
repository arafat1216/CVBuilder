namespace CVBuilder.Application.ViewModels.Company
{
    public class CVRequestViewModel
    {
        public Guid CompanyId { get; set; }
        public string? SearchBySkill { get; set; }
        public string? searchByDegree { get; set; }
        public string? searchByProject { get; set; }
    }
}
