using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("event");

            builder.HasKey(r => r.EventId);

            // Properties
            builder.Property(r => r.EventId)
            .IsRequired()
            .HasColumnName("event_id");

            builder.Property(r => r.Name)
            .IsRequired()
            .HasColumnName("event_name");

            builder.Property(r => r.Capacity)
            .IsRequired()
            .HasColumnName("event_capacity");

            builder.Property(r => r.State)
            .IsRequired()
            .HasColumnName("event_state");

            builder.Property(r => r.EventPoints)
            .IsRequired()
            .HasColumnName("event_points");

            builder.Property(r => r.Date)
            .IsRequired()
            .HasColumnName("event_date");

            builder.Property(r => r.Sponsorship)
            .IsRequired()
            .HasColumnName("event_sponsorship");

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
                    j.ToTable("event_attendance");
                    j.HasKey(t => new{t.EventId, t.UserCc});
                }
            );
        }
    }
}