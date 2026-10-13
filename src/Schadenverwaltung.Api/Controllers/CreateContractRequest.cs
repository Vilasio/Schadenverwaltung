using Schadenverwaltung.Domain.Enums;

namespace Schadenverwaltung.Api.Controllers;

public record CreateContractRequest(
    string PolicyHolder,
    LineOfBusiness LineOfBusiness,
    DateOnly StartDate);