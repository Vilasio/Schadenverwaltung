using Microsoft.EntityFrameworkCore;
using Schadenverwaltung.Infrastructure.Persistence;

namespace Schadenverwaltung.Infrastructure.Numbering;

public class NumberGenerator : INumberGenerator
{
    private readonly SchadenverwaltungDbContext _dbContext;

    public NumberGenerator(SchadenverwaltungDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> NextContractNumberAsync()
    {
        var next = await _dbContext.Database.NextSequenceValueAsync("ContractNumbers");

        return $"V-{next:D6}";
    }

    public async Task<string> NextClaimNumberAsync()
    {
        var next = await _dbContext.Database.NextSequenceValueAsync("ClaimNumbers");

        var year = DateTime.Now.Year.ToString();

        return $"S-{year}-{next:D6}";
    }
}