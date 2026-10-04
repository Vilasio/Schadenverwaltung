using Schadenverwaltung.Domain.Entities;
using Schadenverwaltung.Domain.Enums;

namespace Schadenverwaltung.Tests.Domain;

public class ClaimTests
{
    // TODO 1: Gültigen Schaden erzeugen und zurückgeben (startet als Reported).
    //         Werte frei wählen, der Konstruktor muss sie nur akzeptieren.
    private static Claim CreateClaim()
    {
        return new Claim(1, "CLAIM-0001", new DateOnly(2026, 10, 1),
            "Dies ist ein Testschaden aus dem Unit-Test", 1000m);
    }
    
    // TODO 2: Gültige Zahlung erzeugen und zurückgeben.
    private static Payment CreatePayment()
    {
        return new Payment(100m, new DateOnly(2026, 10, 1), "PAYEE-0001");
    }

    private static Claim CreateClaimInStatus(ClaimStatus target)
    {
        var claim = CreateClaim();
        
        switch (target)
        {
            case ClaimStatus.Reported: break;
            case ClaimStatus.UnderReview:
                claim.ChangeStatus(ClaimStatus.UnderReview);
                break;
            case ClaimStatus.Settled: 
                claim.ChangeStatus(ClaimStatus.UnderReview);
                claim.ChangeStatus(ClaimStatus.Settled);
                break;
            case ClaimStatus.Rejected:
                claim.ChangeStatus(ClaimStatus.Rejected);
                break;
            case ClaimStatus.Closed:
                claim.ChangeStatus(ClaimStatus.Rejected);
                claim.ChangeStatus(ClaimStatus.Closed);
                break;
        }

        return claim;
    }

    [Fact]
    public void AddPayment_StatusReported_AddsPayment()
    {
        var claim = CreateClaim();
        var payment = CreatePayment();
        
        claim.AddPayment(payment);
        
        var single = Assert.Single(claim.Payments);
        Assert.Same(payment, single);
    }

    [Theory]
    [InlineData(ClaimStatus.Reported)]
    [InlineData(ClaimStatus.UnderReview)]
    [InlineData(ClaimStatus.Settled)]
    public void AddPayment_AllowedStatus_AddsPayment(ClaimStatus status)
    {
        var claim = CreateClaimInStatus(status);
        var payment = CreatePayment();
        
        claim.AddPayment(payment);

        var single = Assert.Single(claim.Payments);
        Assert.Same(payment, single);
    }

    [Theory]
    [InlineData(ClaimStatus.Rejected)]
    [InlineData(ClaimStatus.Closed)]
    public void AddPayment_ForbiddenStatus_ThrowsAndAddsNothing(ClaimStatus status)
    {
        var claim = CreateClaimInStatus(status);
        var payment = CreatePayment();
        
        Assert.Throws<InvalidOperationException>(() => claim.AddPayment(payment));
        
        Assert.Empty(claim.Payments);
    }
}