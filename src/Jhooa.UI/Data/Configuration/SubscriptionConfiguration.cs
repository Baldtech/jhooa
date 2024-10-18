using Jhooa.UI.Features.Dreams.Models;
using Jhooa.UI.Features.Subscriptions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jhooa.UI.Data.Configuration;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.StripePaymentIntentId)
            .HasMaxLength(100);
        builder.Property(b => b.StripeSubscriptionId)
            .HasMaxLength(100);
        builder.Property(b => b.StripeSessionCheckoutId)
            .HasMaxLength(100);
    }
}