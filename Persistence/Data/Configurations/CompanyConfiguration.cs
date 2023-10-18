using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");

            builder.HasKey(r => r.CompanyId);

            // Properties
            builder.Property(r => r.CompanyId)
            .IsRequired()
            .HasColumnName("CompanyId");

            builder.Property(r => r.Name)
            .IsRequired()
            .HasColumnName("CompanyName");

            builder.Property(r => r.Contact)
            .IsRequired()
            .HasColumnName("CompanyContact");
        }
    }
}