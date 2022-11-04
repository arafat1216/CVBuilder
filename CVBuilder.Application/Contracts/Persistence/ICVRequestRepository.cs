namespace CVBuilder.Application.Contracts.Persistence
{
    public interface ICVRequestRepository : IAsyncRepository<Domain.Entities.CVRequest>
    {
        Task<Domain.Entities.CVRequest?> GetCVRequest(Guid companyId,string currentDate);  
    }
}
