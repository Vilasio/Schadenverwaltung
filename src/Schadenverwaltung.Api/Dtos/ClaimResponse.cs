using Schadenverwaltung.Domain.Enums;

namespace Schadenverwaltung.Api.Dtos;

public record ClaimResponse(
    int Id,
    int ContractId,
    string ClaimNumber,
    DateOnly DateOfLoss,
    string Description,
    ClaimStatus Status,
    Decimal Reserve);