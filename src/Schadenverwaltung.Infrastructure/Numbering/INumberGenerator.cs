namespace Schadenverwaltung.Infrastructure.Numbering;

public interface INumberGenerator
{
    Task<string> NextContractNumberAsync();
    
    Task<string> NextClaimNumberAsync();
}