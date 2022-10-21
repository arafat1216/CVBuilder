namespace CVBuilder.Application.Dtos.ResourceRequests
{
    public class ResourceRequestDetailsDto
    {
        public int RequestId { get; set; }
        public Guid AppliedBy { get; set; }
        public string RequestType { get; set; }
        public string ResourceType { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public Guid? ReviewedBy { get; set; }
        public Object ResourceDetails { get; set; }
        //public DegreeUpdateRequestDto? DegreeUpdateRequest { get; set; }
        //public PersonalDetailsUpdateRequestDto? PersonalDetailsUpdateRequest { get; set; }
        //public ProjectUpdateRequestDto? ProjectUpdateRequest { get; set; }
        //public SkillUpdateRequestDto? SkillUpdateRequest { get; set; }
        //public WorkExperienceUpdateRequestDto? WorkExperienceUpdateRequest{ get; set; }
    }
}
