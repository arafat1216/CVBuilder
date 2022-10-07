using CVBuilder.Application.Dtos.Degree;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Queries.GetDegreeDetails
{
    public class GetDegreeDetailsQuery : IRequest<DegreeDetailsDto>
    {
        public Guid EmployeeId { get; set; }
        public int DegreeId { get; set; }
    }
}
