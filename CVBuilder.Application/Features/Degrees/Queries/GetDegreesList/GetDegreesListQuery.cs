using CVBuilder.Application.Dtos.Degree;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Queries.GetDegreesList
{
    public class GetDegreesListQuery : IRequest<List<DegreesListDto>>
    {
        public Guid EmployeeId { get; set; }
    }
}
