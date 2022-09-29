namespace CVBuilder.Application.Features.Degrees.Commands.AddDegree
{
    public class AddDegreeCommandResponse
    {
        public Guid EmployeeId { get; set; }
        public int DegreeId { get; set; }
        public string Name { get; set; }
        public string Institute { get; set; }
    }
}
