using CVBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.Infrastructure.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.OwnsOne(p => p.ProjectDetails,
                navigationBuilder =>
                {
                    navigationBuilder.Property(p => p.Name).HasColumnName("Name");
                    navigationBuilder.Property(p => p.Description).HasColumnName("Description");
                    navigationBuilder.Property(p => p.Link).HasColumnName("Link");
                });
        }
    }
}
