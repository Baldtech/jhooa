using Jhooa.UI.Data.Configuration;
using Jhooa.UI.Features.ContactForm.Models;
using Jhooa.UI.Features.Dreams.Models;
using Jhooa.UI.Features.Events.Models;
using Jhooa.UI.Features.Identity.Models;
using Jhooa.UI.Features.Subscriptions.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jhooa.UI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<Dream> Dreams { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    
    public DbSet<ContactFormSubmission> ContactFormSubmissions { get; set; }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(IdentityConfiguration).Assembly);
    }
}