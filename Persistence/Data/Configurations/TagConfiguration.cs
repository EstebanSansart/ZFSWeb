using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");

            builder.HasKey(r => r.TagId);

            // Properties
            builder.Property(r => r.TagId)
            .IsRequired()
            .HasColumnName("TagId");

            builder.Property(r => r.Name)
            .IsRequired()
            .HasColumnName("TagName");

            builder.Property(r => r.Description)
            .IsRequired()
            .HasColumnName("TagDescription");

            // User - Tag

            builder
            .HasMany(r => r.Users)
            .WithMany(p => p.Tags)
            .UsingEntity<UserTag>(

                j => j
                .HasOne(pt => pt.User)
                .WithMany(t => t.UserTags)
                .HasForeignKey(ut => ut.UserCc),

                j => j
                .HasOne(et => et.Tag)
                .WithMany(e => e.UserTags)
                .HasForeignKey(te => te.TagId),

                j =>
                {
                    j.ToTable("UserTag");
                    j.HasKey(t => new{t.TagId, t.UserCc});
                }
            );
        }
    }
}