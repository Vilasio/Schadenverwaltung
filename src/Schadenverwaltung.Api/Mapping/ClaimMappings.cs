using Schadenverwaltung.Api.Dtos;
using Schadenverwaltung.Domain.Entities;

namespace Schadenverwaltung.Api.Mapping;

public static class ClaimMappings
{
    public static ClaimResponse ToResponse(this Claim claim) =>
        new ClaimResponse(
            claim.Id,
            claim.ContractId,
            claim.ClaimNumber,
            claim.DateOfLoss,
            claim.Description,
            claim.Status,
            claim.Reserve);
}