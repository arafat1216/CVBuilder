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
        public DegreeUpdateRequest? DegreeUpdateRequest { get; set; } 
        public PersonalDetailsUpdateRequest? PersonalDetailsUpdateRequest { get; set; } 
        public ProjectUpdateRequest? ProjectUpdateRequest { get; set; } 
        public SkillUpdateRequest? SkillUpdateRequest { get; set; } 
        public WorkExperienceUpdateRequest? WorkExperienceUpdateRequest { get; set; } 

    }
}
