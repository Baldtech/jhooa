using Jhooa.UI.Features.ContactForm.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jhooa.UI.Data.Configuration;

public class ContactFormConfiguration : IEntityTypeConfiguration<ContactFormSubmission>
{
    public void Configure(EntityTypeBuilder<ContactFormSubmission> builder)
    {
        builder.Property(b => b.Sender)
            .HasMaxLength(ContactFormSubmission.SenderMaxLength)
            .IsRequired();
        builder.Property(b => b.Subject)
            .HasMaxLength(ContactFormSubmission.SubjectMaxLength)
            .IsRequired();
        builder.Property(b => b.Content)
            .HasMaxLength(ContactFormSubmission.ContentMaxLength)
            .IsRequired();
    }
}