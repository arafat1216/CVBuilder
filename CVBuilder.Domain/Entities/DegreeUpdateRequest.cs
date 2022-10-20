namespace CVBuilder.Domain.Entities
{
    public class DegreeUpdateRequest
    {
        public int Id { get; set; }
        public int? DegreeId { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; } 
        public string? Institute { get; set; }

        // Navigation Property
        public int RequestId { get; set; }
    }
}
