using Schadenverwaltung.Api.Dtos;
using Schadenverwaltung.Domain.Entities;

namespace Schadenverwaltung.Api.Mapping;

public static class ContractMappings
{
    public static ContractResponse ToResponse(this Contract contract) =>
        new ContractResponse(
            contract.Id,
            contract.ContractNumber,
            contract.PolicyHolder,
            contract.LineOfBusiness,
            contract.StartDate,
            contract.Status);
}