using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schadenverwaltung.Domain.Entities;

namespace Schadenverwaltung.Infrastructure.Persistence.Configurations;

public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.Property(c => c.ClaimNumber).HasMaxLength(20);
        builder.HasIndex(c => c.ClaimNumber).IsUnique();

        builder.Property(c => c.Description).HasMaxLength(2000);

        builder.Property(c => c.Reserve).HasPrecision(18, 2);
        
        builder.HasOne<Contract>()
            .WithMany()
            .HasForeignKey(c => c.ContractId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<Payment>(c => c.Payments)
            .WithOne()
            .HasForeignKey(p => p.ClaimId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(c => c.Payments)
            .HasField("_payments");
    }
}