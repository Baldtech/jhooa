using Jhooa.UI.Features.Dreams.Models;
using Jhooa.UI.Features.Events.Models;
using Jhooa.UI.Features.Identity.Models;
using Jhooa.UI.Features.Subscriptions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jhooa.UI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<Dream> Dreams { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<SubscriptionHistory> SubscriptionHistories { get; set; }
    


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Dream>()
            .Property(b => b.Title)
            .HasMaxLength(Dream.TitleMaxLength)
            .IsRequired();

        builder.Entity<Event>()
            .Property(b => b.Title)
            .HasMaxLength(Event.TitleMaxLength)
            .IsRequired();
        builder.Entity<Event>()
            .Property(b => b.Description)
            .HasMaxLength(Event.DescriptionMaxLength)
            .IsRequired();
        builder.Entity<Event>()
            .Property(b => b.StartDate)
            .IsRequired();
        builder.Entity<Event>()
            .Property(b => b.EndDate)
            .IsRequired();
        builder.Entity<Event>()
            .Property(b => b.ImageUrl)
            .HasMaxLength(Event.ImageUrlMaxLength);
        builder.Entity<Event>()
            .HasMany(e => e.Registrations)
            .WithOne(e => e.Event)
            .HasForeignKey(e => e.EventId)
            .IsRequired();

        builder.Entity<Registration>()
            .HasKey(c => new { c.UserId, c.EventId });
        builder.Entity<Registration>()
            .Property(b => b.UserId)
            .IsRequired();
        builder.Entity<Registration>()
            .Property(b => b.EventId)
            .IsRequired();
        builder.Entity<Registration>()
            .Property(b => b.RegistrationDate)
            .IsRequired();


        builder.Entity<ApplicationUser>()
            .HasMany(e => e.Registrations)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        builder.Entity<ApplicationUser>()
            .HasMany(e => e.Dreams)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        
        builder.Entity<SubscriptionHistory>()
            .Property(b => b.StripeCheckoutSessionId)
            .HasMaxLength(SubscriptionHistory.StripeCheckoutSessionIdMaxLength)
            .IsRequired();
    }
}