using CVBuilder.Application.Dtos.Degree;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Queries.GetDegreesList
{
    public class GetDegreesListQuery : IRequest<List<DegreeDetailsDto>>
    {
        public Guid EmployeeId { get; set; }
    }
}
