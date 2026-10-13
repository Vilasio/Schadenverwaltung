namespace Schadenverwaltung.Infrastructure.Numbering;

public interface IContractNumberGenerator
{
    Task<string> NextNumberAsync();
}