using CVBuilder.Application.ViewModels;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Queries.GetDegreeDetails
{
    public class GetDegreeDetailsQuery : IRequest<DegreeViewModel>
    {
        public Guid EmployeeId { get; set; }
        public int DegreeId { get; set; }
    }
}
