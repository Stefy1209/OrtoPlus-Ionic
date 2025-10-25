using Data.Domain.Account;

namespace Data.Domain;

public class Review
{
    public Guid ReviewId { get; init; }
    public string Comment { get; private set; } = string.Empty;
    public int Rating { get; private set; } = 1;
    public DateTimeOffset Date { get; private set; } = DateTimeOffset.UtcNow;
    
    public Guid UserAccountId { get; private set; }
    public UserAccount UserAccount { get; private set; } = null!;

    public Guid ClinicId { get; private set; }
    public Clinic Clinic { get; private set; } = null!;

    // EF Core
    private Review() { }

    public Review(string comment, int rating, Guid userAccountId, Guid clinicId)
    {
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment cannot be null or empty.", nameof(comment));
        if (rating < 1 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");

        ReviewId = Guid.NewGuid();
        Comment = comment;
        Rating = rating;
        UserAccountId = userAccountId;
        ClinicId = clinicId;
    }
}
