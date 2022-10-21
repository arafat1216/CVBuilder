namespace CVBuilder.Application.Dtos.ResourceRequests
{
    public class DegreeUpdateRequestDto
    {
        public int Id { get; set; }
        public int? DegreeId { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public string? Institute { get; set; }
    }
}
