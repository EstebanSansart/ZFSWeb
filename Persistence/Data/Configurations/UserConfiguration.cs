using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(r => r.UserCc);

            // Properties
            builder.Property(r => r.UserCc)
            .IsRequired()
            .HasColumnName("user_cc");

            builder.Property(r => r.Name)
            .IsRequired()
            .HasColumnName("user_name");

            builder.Property(r => r.Age)
            .IsRequired()
            .HasColumnName("user_age");

            builder.Property(r => r.Contact)
            .IsRequired()
            .HasColumnName("user_contact");

            // Relationships
            builder.HasOne(x => x.Company)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.CompanyId);

            builder.HasOne(x => x.Gender)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.GenderId);

            builder.HasOne(x => x.Level)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.LevelId);
        }
    }
}