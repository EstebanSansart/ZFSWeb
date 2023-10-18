using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("company");

            builder.HasKey(r => r.CompanyId);

            // Properties
            builder.Property(r => r.CompanyId)
            .IsRequired()
            .HasColumnName("company_id");

            builder.Property(r => r.Name)
            .IsRequired()
            .HasColumnName("company_name");

            builder.Property(r => r.Contact)
            .IsRequired()
            .HasColumnName("company_contact");

            builder.HasData(
                new{
                    CompanyId = 1,
                    Name = "Solvo",
                    Contact = "solvoq@ec.com"
                }
            );
        }
    }
}