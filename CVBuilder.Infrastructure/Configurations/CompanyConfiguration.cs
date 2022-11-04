using CVBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.Infrastructure.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasOne(c => c.CompanyDetails).WithOne().HasForeignKey<CompanyDetails>(cd => cd.CompanyId);

            builder.HasMany(c => c.CVRequests).WithOne().HasForeignKey(c => c.CompanyId);
        }
    }
}
