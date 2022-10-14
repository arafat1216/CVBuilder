using MediatR;

namespace CVBuilder.Application.Features.Degrees.Commands.UpdateDegree
{
    public class UpdateDegreeCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public int DegreeId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Institute { get; set; }
    }
}
