namespace CVBuilder.Application.Dtos.ResourceRequests
{
    public class ResourceRequestsListDto
    {
        public int RequestId { get; set; }
        public Guid AppliedBy { get; set; }
        public string RequestType { get; set; }
        public string ResourceType { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public Guid? ReviewedBy { get; set; }
    }
}
