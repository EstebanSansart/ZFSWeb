using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(r => r.UserCc);

            // Properties
            builder.Property(r => r.UserCc)
            .IsRequired()
            .HasColumnName("UserCc");

            builder.Property(r => r.Name)
            .IsRequired()
            .HasColumnName("UserName");

            builder.Property(r => r.Age)
            .IsRequired()
            .HasColumnName("UserAge");

            builder.Property(r => r.Contact)
            .IsRequired()
            .HasColumnName("UserContact");

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