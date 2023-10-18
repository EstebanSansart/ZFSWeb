using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.ToTable("Level");

            builder.HasKey(r => r.LevelId);

            // Properties
            builder.Property(r => r.LevelId)
            .IsRequired()
            .HasColumnName("LevelId");

            builder.Property(r => r.LevelNumber)
            .IsRequired()
            .HasColumnName("LevelNumber");

            builder.Property(r => r.CurrentPoints)
            .IsRequired()
            .HasColumnName("LevelCurrentPoints");
        }
    }
}