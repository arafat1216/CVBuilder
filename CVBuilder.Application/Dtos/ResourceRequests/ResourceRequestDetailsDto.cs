using Newtonsoft.Json;

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

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DegreeUpdateRequestDto? DegreeUpdateRequest { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PersonalDetailsUpdateRequestDto? PersonalDetailsUpdateRequest { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ProjectUpdateRequestDto? ProjectUpdateRequest { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SkillUpdateRequestDto? SkillUpdateRequest { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WorkExperienceUpdateRequestDto? WorkExperienceUpdateRequest { get; set; }
    }
}
