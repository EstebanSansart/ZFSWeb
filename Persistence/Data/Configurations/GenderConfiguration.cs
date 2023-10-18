using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Gender");

            builder.HasKey(r => r.GenderId);

            // Properties
            builder.Property(r => r.GenderId)
            .IsRequired()
            .HasColumnName("GenderId");

            builder.Property(r => r.GenderType)
            .IsRequired()
            .HasColumnName("GenderType");
        }
    }
}