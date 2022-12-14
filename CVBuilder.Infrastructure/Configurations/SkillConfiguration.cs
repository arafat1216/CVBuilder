using CVBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.Infrastructure.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.OwnsOne(s => s.SkillDetails,
                navigationBuilder =>
                {
                    navigationBuilder.Property(s => s.Name).HasColumnName("Name");
                });

        }
    }
}
