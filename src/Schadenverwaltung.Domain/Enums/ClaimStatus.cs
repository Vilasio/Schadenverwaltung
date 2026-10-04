namespace Schadenverwaltung.Domain.Enums;

public enum ClaimStatus
{
    Reported = 1,
    UnderReview = 2,
    Settled = 3,
    Rejected = 4,
    Closed = 5
}

public static class ClaimStatusExtensions
{
    public static string ToDisplayText(this ClaimStatus status) => status switch
    {
        ClaimStatus.Reported    => "Gemeldet",
        ClaimStatus.UnderReview => "In Prüfung",
        ClaimStatus.Settled     => "Reguliert",
        ClaimStatus.Rejected    => "Abgelehnt",
        ClaimStatus.Closed      => "Abgeschlossen",
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
    };
}