using Schadenverwaltung.Domain.Enums;

namespace Schadenverwaltung.Domain.Entities;

public class Contract
{
    public int Id { get; private set; }
    public string ContractNumber { get; private set; }
    public string PolicyHolder { get; private set; }
    public LineOfBusiness LineOfBusiness { get; private set;  }
    public DateOnly StartDate { get; private set; }
    public ContractStatus Status { get; private set; }

    public Contract(string contractNumber, string policyHolder, LineOfBusiness lineOfBusiness, DateOnly startDate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(contractNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(policyHolder);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(policyHolder.Length, 200);
        
        ContractNumber = contractNumber;
        PolicyHolder = policyHolder;
        LineOfBusiness = lineOfBusiness;
        StartDate = startDate;
        Status = ContractStatus.Active; 
    }
}