using CVBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.Infrastructure.Configurations
{
    public class DegreeConfiguration : IEntityTypeConfiguration<Degree>
    {
        public void Configure(EntityTypeBuilder<Degree> builder)
        {
            builder.OwnsOne(d => d.DegreeDetails,
                navigationBuilder =>
                {
                    navigationBuilder.Property(d => d.Name).HasColumnName("Name");
                    navigationBuilder.Property(d => d.Institute).HasColumnName("Institute");
                    navigationBuilder.Property(d => d.Subject).HasColumnName("Subject").HasDefaultValue("");
                });
              
        }
    }
}
