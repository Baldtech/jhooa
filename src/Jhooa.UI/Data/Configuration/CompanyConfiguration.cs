using Jhooa.UI.Features.Companies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jhooa.UI.Data.Configuration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>, IEntityTypeConfiguration<CompanyCode>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasMany(e => e.Codes)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId)
            .IsRequired();
        
        builder.Property(b => b.Name)
            .IsRequired();
    }

    public void Configure(EntityTypeBuilder<CompanyCode> builder)
    {
        builder.Property(b => b.Code)
            .IsRequired();
        builder.Property(b => b.CompanyId)
            .IsRequired();
        builder.Property(b => b.CreatedAt)
            .IsRequired();
    }
}