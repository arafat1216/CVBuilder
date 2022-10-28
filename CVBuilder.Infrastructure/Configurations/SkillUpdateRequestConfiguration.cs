using CVBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.Infrastructure.Configurations
{
    public class SkillUpdateRequestConfiguration : IEntityTypeConfiguration<SkillUpdateRequest>
    {
        public void Configure(EntityTypeBuilder<SkillUpdateRequest> builder)
        {
            builder.OwnsOne(s => s.SkillDetails,
              navigationBuilder =>
                {
                    navigationBuilder.Property(s => s.Name).HasColumnName("Name");
                });
        }
    }
}
