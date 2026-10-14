using System.ComponentModel.DataAnnotations;
using Schadenverwaltung.Domain.Enums;
namespace Schadenverwaltung.Api.Dtos;

public record ChangeClaimStatusRequest(
    [Required]ClaimStatus? Status
);