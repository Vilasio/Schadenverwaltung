using Schadenverwaltung.Domain.Entities;
using Schadenverwaltung.Domain.Enums;

namespace Schadenverwaltung.Tests.Domain;

public class ClaimTests
{
    private static Claim CreateClaim(decimal reserve = 1000m)
    {
        return new Claim(1, "CLAIM-0001", new DateOnly(2026, 10, 1),
            "Dies ist ein Testschaden aus dem Unit-Test", reserve);
    }

    private static Payment CreatePayment(decimal amount = 100m)
    {
        return new Payment(amount, new DateOnly(2026, 10, 1), "PAYEE-0001");
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

    [Theory]
    [InlineData(300, false)] // unter Reserve
    [InlineData(400, false)] // genau Reserve
    [InlineData(500, true)] // über Reserve
    public void AddPayment_SumComparedToReserve_ReportsExceeded(int newAmount, bool expectedExceeded)
    {
        //Arrange
        const decimal initialAmount = 600m;
        var claim = CreateClaim(reserve: 1000m);
        claim.AddPayment(CreatePayment(initialAmount));

        //Act
        var result = claim.AddPayment(CreatePayment(newAmount));

        //Assert
        Assert.Equal(expectedExceeded, result.ReserveExceeded);
        Assert.Equal(initialAmount + newAmount, result.TotalPaid);
    }
}