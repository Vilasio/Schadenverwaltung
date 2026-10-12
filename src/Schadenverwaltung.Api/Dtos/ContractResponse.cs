namespace Schadenverwaltung.Api.Dtos;

public record ContractResponse(
    int Id,
    string ContractNumber,
    string PolicyHolder,
    string LineOfBusiness,
    DateOnly StartDate,
    string Status );
