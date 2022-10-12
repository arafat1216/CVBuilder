using MediatR;

namespace CVBuilder.Application.Features.Degrees.Commands.PartialUpdateDegree
{
    public class PartialUpdateDegreeCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public int DegreeId { get; set; }
        public string? Name { get; set; }
        public string? Institute { get; set; }
    }
}
