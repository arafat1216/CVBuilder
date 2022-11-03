using CVBuilder.Application.Dtos.UpdateCVRequestServiceResponse;
using CVBuilder.Application.ViewModels.UpdateCV;

namespace CVBuilder.Application.Contracts.UpdateCVRequest
{
    public interface IUpdateCVRequestService
    {
        Task<List<Response>> HandleRequest(UpdateCVViewModel updateCVViewModel);
    }
}
