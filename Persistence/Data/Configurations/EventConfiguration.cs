using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");

            builder.HasKey(r => r.EventId);

            // Properties
            builder.Property(r => r.EventId)
            .IsRequired()
            .HasColumnName("EventId");

            builder.Property(r => r.Name)
            .IsRequired()
            .HasColumnName("EventName");

            builder.Property(r => r.Capacity)
            .IsRequired()
            .HasColumnName("EventCapacity");

            builder.Property(r => r.State)
            .IsRequired()
            .HasColumnName("EventState");

            builder.Property(r => r.EventPoints)
            .IsRequired()
            .HasColumnName("EventPoints");

            builder.Property(r => r.Date)
            .IsRequired()
            .HasColumnName("EventDate");

            builder.Property(r => r.Sponsorship)
            .IsRequired()
            .HasColumnName("EventSponsorship");

            // User - Event
            builder
            .HasMany(r => r.Users)
            .WithMany(p => p.Events)
            .UsingEntity<EventAttendance>(

                j => j
                .HasOne(pt => pt.User)
                .WithMany(t => t.EventAttendances)
                .HasForeignKey(ut => ut.UserCc),

                j => j
                .HasOne(et => et.Event)
                .WithMany(e => e.EventAttendances)
                .HasForeignKey(te => te.EventId),

                j =>
                {
                    j.ToTable("EventAttendance");
                    j.HasKey(t => new{t.EventId, t.UserCc});
                }
            );
        }
    }
}