using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
            builder.ToTable("reaction");

            builder.HasKey(r => r.ReactionId);

            // Properties
            builder.Property(r => r.ReactionId)
            .IsRequired()
            .HasColumnName("reaction_id");

            builder.Property(r => r.Name)
            .IsRequired()
            .HasColumnName("reaction_name");

            // User - Reaction
            builder
            .HasMany(r => r.Users)
            .WithMany(p => p.Reactions)
            .UsingEntity<UserReaction>(

                j => j
                .HasOne(pt => pt.User)
                .WithMany(t => t.UserReactions)
                .HasForeignKey(ut => ut.UserCc),

                j => j
                .HasOne(et => et.Reaction)
                .WithMany(e => e.UserReactions)
                .HasForeignKey(te => te.ReactionId),

                j =>
                {
                    j.ToTable("user_reaction");
                    j.HasKey(t => new{t.ReactionId, t.UserCc});
                }
            );
        }
    }
}