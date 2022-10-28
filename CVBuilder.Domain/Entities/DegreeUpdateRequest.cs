using CVBuilder.Domain.ValueObjects;

namespace CVBuilder.Domain.Entities
{
    public class DegreeUpdateRequest
    {
        public int Id { get; set; }
        public int? DegreeId { get; set; }

        public DegreeDetails DegreeDetails { get; set; }

        // Navigation Property
        public int RequestId { get; set; }
    }
}
