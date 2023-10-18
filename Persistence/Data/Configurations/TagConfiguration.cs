using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("tag");

            builder.HasKey(r => r.TagId);

            // Properties
            builder.Property(r => r.TagId)
            .IsRequired()
            .HasColumnName("tag_id");

            builder.Property(r => r.Name)
            .IsRequired()
            .HasColumnName("tag_name");

            builder.Property(r => r.Description)
            .IsRequired()
            .HasColumnName("tag_description");

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
                    j.ToTable("user_tag");
                    j.HasKey(t => new{t.TagId, t.UserCc});
                }
            );
        }
    }
}