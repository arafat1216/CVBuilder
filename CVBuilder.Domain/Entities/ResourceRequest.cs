namespace CVBuilder.Domain.Entities
{
    public class ResourceRequest
    {
        
        public int RequestId { get; set; }

        public Guid AppliedBy { get; set; }

        public string RequestType { get; set; }
        
        public string ResourceType { get; set; }

        public Guid? ReviewedBy { get; set; }

        public string Status { get; set; } = "Pending";
        
        public string Reason { get; set; }


        // Navigation Property
        public DegreeUpdateRequest DegreeUpdateRequest { get; set; } = new DegreeUpdateRequest();
        public PersonalDetailsUpdateRequest PersonalDetailsUpdateRequest { get; set; } = new PersonalDetailsUpdateRequest();
        public ProjectUpdateRequest ProjectUpdateRequest { get; set; } = new ProjectUpdateRequest();
        public SkillUpdateRequest SkillUpdateRequest { get; set; } = new SkillUpdateRequest();
        public WorkExperienceUpdateRequest WorkExperienceUpdateRequest { get; set; } = new WorkExperienceUpdateRequest();

    }
}
