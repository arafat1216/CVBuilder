using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.UpdateResourceManager
{
    public interface IUpdateResourceService
    {
        Task UpdateResource(ResourceRequest resourceRequest);
    }
}
