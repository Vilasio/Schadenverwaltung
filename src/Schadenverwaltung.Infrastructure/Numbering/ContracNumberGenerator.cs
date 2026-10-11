using Schadenverwaltung.Infrastructure.Persistence;

namespace Schadenverwaltung.Infrastructure.Numbering;

public class ContractNumberGenerator : IContractNumberGenerator
{
    private readonly SchadenverwaltungDbContext _dbContext; 

    public ContractNumberGenerator(SchadenverwaltungDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public string NextNumber()
    {
        throw new NotImplementedException();
    }
}