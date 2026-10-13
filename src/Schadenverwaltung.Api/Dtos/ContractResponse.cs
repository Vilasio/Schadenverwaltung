using Schadenverwaltung.Domain.Enums;

namespace Schadenverwaltung.Api.Dtos;

public record ContractResponse(
    int Id,
    string ContractNumber,
    string PolicyHolder,
    LineOfBusiness LineOfBusiness,
    DateOnly StartDate,
    ContractStatus Status);