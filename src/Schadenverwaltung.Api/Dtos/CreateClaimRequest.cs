using System.ComponentModel.DataAnnotations;

namespace Schadenverwaltung.Api.Dtos;

public record CreateClaimRequest(
    [Range(1, int.MaxValue)] int ContractId,
    [Required] DateOnly? DateOfLoss,
    [Required, StringLength(2000)] string Description,
    [Range(typeof(decimal), "0", "9999999999999999.99", ParseLimitsInInvariantCulture = true)] decimal Reserve);