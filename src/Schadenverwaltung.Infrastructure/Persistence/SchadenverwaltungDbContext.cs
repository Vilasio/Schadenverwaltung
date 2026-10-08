using Microsoft.EntityFrameworkCore;
using Schadenverwaltung.Domain.Entities;

namespace Schadenverwaltung.Infrastructure.Persistence;

public class SchadenverwaltungDbContext : DbContext
{
    public SchadenverwaltungDbContext(DbContextOptions<SchadenverwaltungDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<Claim> Claims => Set<Claim>();
    public DbSet<Payment> Payments => Set<Payment>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchadenverwaltungDbContext).Assembly);
    }
}