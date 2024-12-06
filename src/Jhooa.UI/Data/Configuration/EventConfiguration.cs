using Jhooa.UI.Features.Events.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jhooa.UI.Data.Configuration;

public class EventConfiguration : 
    IEntityTypeConfiguration<Event>, 
    IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(b => b.Title)
            .HasMaxLength(Event.TitleMaxLength)
            .IsRequired();
        builder.Property(b => b.Description)
            .HasMaxLength(Event.DescriptionMaxLength)
            .IsRequired();
        builder.Property(b => b.StartDate)
            .IsRequired();
        builder.Property(b => b.EndDate)
            .IsRequired();
        builder.Property(b => b.ImageUrl)
            .HasMaxLength(Event.ImageUrlMaxLength);

        builder.HasMany(e => e.Registrations)
            .WithOne(e => e.Event)
            .HasForeignKey(e => e.EventId)
            .IsRequired();
    }

    public void Configure(EntityTypeBuilder<Registration> builder)
    {
        builder.HasKey(c => new { c.UserId, c.EventId });
        builder.Property(b => b.UserId)
            .IsRequired();
        builder.Property(b => b.EventId)
            .IsRequired();
        builder.Property(b => b.RegistrationDate)
            .IsRequired();
    }
}