using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface ICompanyDetailsRepository : IAsyncRepository<CompanyDetails>
    {
        Task<CompanyDetails?> GetCompanyDetailsById(Guid companyId);
    }
}
