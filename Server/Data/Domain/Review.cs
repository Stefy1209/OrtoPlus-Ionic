using Data.Domain.Account;

namespace Data.Domain;

public class Review
{
    public Guid ReviewId { get; } = Guid.NewGuid();
    public string Comment { get; private set; } = string.Empty;
    public int Rating { get; private set; } = 1;
    public DateTimeOffset Date { get; } = DateTimeOffset.UtcNow;
    
    public Guid UserAccountId { get; init; }
    public UserAccount UserAccount { get; init; } = null!;

    public Guid ClinicId { get; init; }
    public Clinic Clinic { get; init; } = null!;

    // EF Core
    private Review() { }

    public Review(string comment, int rating, Guid userAccountId, Guid clinicId)
    {
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment cannot be null or empty.", nameof(comment));
        if (rating < 1 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");

        Comment = comment;
        Rating = rating;
        UserAccountId = userAccountId;
        ClinicId = clinicId;
    }

    public void UpdateComment(string newComment)
    {
        if (string.IsNullOrWhiteSpace(newComment))
            throw new ArgumentException("Comment cannot be null or empty.", nameof(newComment));
        Comment = newComment;
    }

    public void UpdateRating(int newRating)
    {
        if (newRating < 1 || newRating > 5)
            throw new ArgumentOutOfRangeException(nameof(newRating), "Rating must be between 1 and 5.");
        Rating = newRating;
    }
}
