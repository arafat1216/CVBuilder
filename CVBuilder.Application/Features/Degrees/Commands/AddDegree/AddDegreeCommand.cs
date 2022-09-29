using MediatR;

namespace CVBuilder.Application.Features.Degrees.Commands.AddDegree
{
    public class AddDegreeCommand : IRequest<AddDegreeCommandResponse>
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Institute { get; set; }
    }
}
