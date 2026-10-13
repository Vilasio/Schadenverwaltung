using Microsoft.EntityFrameworkCore;
using Schadenverwaltung.Infrastructure.Persistence;

namespace Schadenverwaltung.Infrastructure.Numbering;

public class ContractNumberGenerator : IContractNumberGenerator
{
    private readonly SchadenverwaltungDbContext _dbContext;

    public ContractNumberGenerator(SchadenverwaltungDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> NextNumberAsync()
    {
        var next = await _dbContext.Database
            .SqlQuery<int>($"Select nextval('\"ContractNumbers\"') AS \"Value\"")
            .SingleAsync();

        return $"V-{next:D6}";
    }
}