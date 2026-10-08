using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schadenverwaltung.Domain.Entities;

namespace Schadenverwaltung.Infrastructure.Persistence.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.Property(c => c.ContractNumber).HasMaxLength(20);
        builder.HasIndex(c => c.ContractNumber).IsUnique();
        
        builder.Property(c => c.PolicyHolder).HasMaxLength(200);

    }
}