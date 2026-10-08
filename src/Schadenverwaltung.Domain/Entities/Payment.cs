namespace Schadenverwaltung.Domain.Entities;

public class Payment
{
    public int Id { get; private set; }
    public int ClaimId { get; private set; }
    public decimal Amount { get; private set; }
    public DateOnly PaymentDate { get; private set; }
    public string Payee { get; private set; }

    public Payment(decimal amount, DateOnly paymentDate, string payee)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(amount, 0m);
        ArgumentException.ThrowIfNullOrWhiteSpace(payee);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(payee.Length, 200);
        ArgumentOutOfRangeException.ThrowIfEqual(paymentDate, default);
        
        Amount = amount;
        PaymentDate = paymentDate;
        Payee = payee;

    }
}