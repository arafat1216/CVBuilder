using CVBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.Infrastructure.Configurations
{
    public class WorkExperienceUpdateRequestConfiguration : IEntityTypeConfiguration<WorkExperienceUpdateRequest>
    {
        public void Configure(EntityTypeBuilder<WorkExperienceUpdateRequest> builder)
        {
            builder.OwnsOne(w => w.WorkExperienceDetails,
                navigationBuilder =>
                {
                    navigationBuilder.Property(w => w.Designation).HasColumnName("Designation");
                    navigationBuilder.Property(w => w.Company).HasColumnName("Company");
                    navigationBuilder.Property(w => w.StartDate).HasColumnName("StartDate");
                    navigationBuilder.Property(w => w.EndDate).HasColumnName("EndDate");
                });
        }
    }
}
