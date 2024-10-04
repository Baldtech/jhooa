using Jhooa.UI.Features.Dreams.Models;
using Jhooa.UI.Features.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jhooa.UI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Dream> Dreams { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Dream>()
            .Property(b => b.Title)
            .HasMaxLength(Dream.TitleMaxLength)
            .IsRequired();
    }
}
