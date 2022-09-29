using MediatR;

namespace CVBuilder.Application.Features.Degrees.Commands.DeleteDegree
{
    public class DeleteDegreeCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public int DegreeId { get; set; }
    }
}
