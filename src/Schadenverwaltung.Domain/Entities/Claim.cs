using Schadenverwaltung.Domain.Enums;

namespace Schadenverwaltung.Domain.Entities;

public class Claim
{
    private readonly List<Payment> _payments = [];
    public int Id { get; private set; }
    public int ContractId { get; private set; }
    public IReadOnlyCollection<Payment> Payments => _payments;
    public string ClaimNumber { get; private set; }
    public DateOnly DateOfLoss { get; private set; }
    public string Description { get; private set; }
    public decimal Reserve { get; private set; }
    public ClaimStatus Status { get; private set; }

    public Claim(int contractId, string claimNumber, DateOnly dateOfLoss, 
                    string description, decimal reserve)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(contractId);
        ArgumentException.ThrowIfNullOrWhiteSpace(claimNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(description.Length, 2000);
        ArgumentOutOfRangeException.ThrowIfEqual(dateOfLoss, default);
        ArgumentOutOfRangeException.ThrowIfNegative(reserve);

        ContractId = contractId;
        ClaimNumber = claimNumber;
        DateOfLoss = dateOfLoss;
        Description = description;
        Reserve = reserve;

        Status = ClaimStatus.Reported;
    }

    public void ChangeStatus(ClaimStatus newStatus)
    {
        this.Status = newStatus;
    }

    public void AddPayment(Payment payment)
    {
        ArgumentNullException.ThrowIfNull(payment);

        if (this.Status is  ClaimStatus.Rejected or ClaimStatus.Closed) 
        {
            throw new InvalidOperationException($"Auf diesen Schaden ({this.ClaimNumber}) kann aufgrund des Status " +
                                                $"\"{this.Status.ToDisplayText()}\" keine Auszahlung getätigt werden.");
        }

        
        _payments.Add(payment);
    }
}