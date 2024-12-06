using Jhooa.UI.Features.Dreams.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jhooa.UI.Data.Configuration;

public class DreamConfiguration : IEntityTypeConfiguration<Dream>
{
    public void Configure(EntityTypeBuilder<Dream> builder)
    {
        builder.Property(b => b.Title)
            .HasMaxLength(Dream.TitleMaxLength)
            .IsRequired();
    }
}