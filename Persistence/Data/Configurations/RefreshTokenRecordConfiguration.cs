using System.IO.Compression;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;
public class RefreshTokenRecordConfiguration : IEntityTypeConfiguration<RefreshTokenRecord>
{
    public void Configure(EntityTypeBuilder<RefreshTokenRecord> builder){
        builder.ToTable("refresh_token_record");
        builder.HasKey(x => x.RefreshTokenRecordId);

        // Properties
        builder.Property(x => x.RefreshTokenRecordId)
            .IsRequired()
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("RefreshTokenRecordId");

        builder.Property(x => x.Token)
            .IsRequired()
            .HasColumnName("token")
            .HasMaxLength(500);

        builder.Property(x => x.RefreshToken)
            .IsRequired()
            .HasColumnName("refresh_token")
            .HasMaxLength(200);

        builder.Property(x => x.CreationDate)
            .IsRequired()
            .HasColumnName("creation_date")
            .HasColumnType("DateTime");

        builder.Property(x => x.ExpirationDate)
            .IsRequired()
            .HasColumnName("expiration_date")
            .HasColumnType("DateTime");

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasColumnName("IsActive");

        // Relationships
        builder.HasOne(d => d.User)
            .WithMany(p => p.RefreshTokenRecords)
            .HasForeignKey(d => d.UserCc)
            .HasConstraintName("FK__Record__UserId__24927208");
    }
}