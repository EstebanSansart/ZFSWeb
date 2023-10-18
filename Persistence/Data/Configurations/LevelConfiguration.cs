using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.ToTable("level");

            builder.HasKey(r => r.LevelId);

            // Properties
            builder.Property(r => r.LevelId)
            .IsRequired()
            .HasColumnName("level_id");

            builder.Property(r => r.LevelNumber)
            .IsRequired()
            .HasColumnName("level_number");

            builder.Property(r => r.CurrentPoints)
            .IsRequired()
            .HasColumnName("level_current_points");
        }
    }
}