using CVBuilder.Domain.Entities;
using CVBuilder.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.Infrastructure.Configurations
{
    public class DegreeUpdateRequestConfiguration : IEntityTypeConfiguration<DegreeUpdateRequest>
    {
        public void Configure(EntityTypeBuilder<DegreeUpdateRequest> builder)
        {
            
            builder.OwnsOne(d => d.DegreeDetails,
                navigationBuilder =>
                {
                    navigationBuilder.Property(d => d.Name).HasColumnName("Name");
                    navigationBuilder.Property(d => d.Subject).HasColumnName("Subject");
                    navigationBuilder.Property(d => d.Institute).HasColumnName("Institute");
                });
        }
    }
}
