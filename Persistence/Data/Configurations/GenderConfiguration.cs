using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("gender");

            builder.HasKey(r => r.GenderId);

            // Properties
            builder.Property(r => r.GenderId)
            .IsRequired()
            .HasColumnName("gender_id");

            builder.Property(r => r.GenderType)
            .IsRequired()
            .HasColumnName("gender_type");

            builder.HasData(
                new{
                    GenderId = 1,
                    GenderType = "Hombre"
                }
            );
        }
    }
}