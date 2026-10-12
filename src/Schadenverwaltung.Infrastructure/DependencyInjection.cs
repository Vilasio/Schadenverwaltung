using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Schadenverwaltung.Infrastructure.Numbering;
using Schadenverwaltung.Infrastructure.Persistence;

namespace Schadenverwaltung.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<SchadenverwaltungDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IContractNumberGenerator, ContractNumberGenerator>();


        return services;
    }
}