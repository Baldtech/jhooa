using Jhooa.UI.Data.Configuration;
using Jhooa.UI.Features.Companies.Models;
using Jhooa.UI.Features.Companies.Resources;
using Jhooa.UI.Features.ContactForm.Models;
using Jhooa.UI.Features.Dreams.Models;
using Jhooa.UI.Features.Events.Models;
using Jhooa.UI.Features.Identity.Models;
using Jhooa.UI.Features.Policies.Models;
using Jhooa.UI.Features.Subscriptions.Models;
using Jhooa.UI.Features.Videos.Models;
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

    public DbSet<Video> Videos { get; set; }
    public DbSet<VideoTheme> VideoThemes { get; set; }
    public DbSet<Policy> Policies { get; set; }

    public DbSet<UserView> UsersView { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyCode> CompanyCodes { get; set; }

    
#pragma warning disable MA0051
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#pragma warning restore MA0051
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSeeding((context, _) =>
        {
            var adminRole = context.Set<ApplicationRole>()
                .FirstOrDefault(b => b.Name == "Admin");
            if (adminRole == null)
            {
                context.Set<ApplicationRole>().Add(new ApplicationRole { Id = Guid.NewGuid(), Name = "Admin" });
            }

            var policies = context.Set<Policy>().ToList();
            if (!policies.Any(p => p.Type == PolicyType.GeneralTerms))
            {
                context.Set<Policy>().Add(new Policy
                {
                    Type = PolicyType.GeneralTerms,
                    TitleFr = "Conditions générales",
                    TitleEn = "General terms",
                    ContentFr = "Conditions générales",
                    ContentEn = "General terms",
                    LastUpdate = DateTimeOffset.UtcNow
                });
            }

            if (!policies.Any(p => p.Type == PolicyType.LegalNotices))
            {
                context.Set<Policy>().Add(new Policy
                {
                    Type = PolicyType.LegalNotices,
                    TitleFr = "Mentions légales",
                    TitleEn = "Legal notices",
                    ContentFr = "Mentions légales",
                    ContentEn = "Legal notices",
                    LastUpdate = DateTimeOffset.UtcNow
                });
            }
            
            if (!policies.Any(p => p.Type == PolicyType.PrivacyPolicy))
            {
                context.Set<Policy>().Add(new Policy
                {
                    Type = PolicyType.PrivacyPolicy,
                    TitleFr = "Privacy policy",
                    TitleEn = "Politique de confidentialité",
                    ContentFr = "Privacy policy",
                    ContentEn = "Politique de confidentialité",
                    LastUpdate = DateTimeOffset.UtcNow
                });
            }
            
            if (!policies.Any(p => p.Type == PolicyType.CookiePolicy))
            {
                context.Set<Policy>().Add(new Policy
                {
                    Type = PolicyType.CookiePolicy,
                    TitleFr = "Politique cookies",
                    TitleEn = "Cookie policy",
                    ContentFr = "Politique cookies",
                    ContentEn = "Cookie policy",
                    LastUpdate = DateTimeOffset.UtcNow
                });
            }

            context.SaveChanges();
        });
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(IdentityConfiguration).Assembly);
        
        builder.Entity<UserView>().ToView("UserWithAdminCheck").HasNoKey();

    }
}