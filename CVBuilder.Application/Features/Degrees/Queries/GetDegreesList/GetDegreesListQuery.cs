using CVBuilder.Application.ViewModels.Degree;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Queries.GetDegreesList
{
    public class GetDegreesListQuery : IRequest<List<DegreeViewModel>>
    {
        public Guid EmployeeId { get; set; }
    }
}
