namespace Business.DTOs;

public class ReviewDto(Guid reviewId, string comment, int rating, DateTimeOffset date)
{
    public Guid ReviewId { get; set; } = reviewId;
    public string Comment { get; init; } = comment;
    public int Rating { get; init; } = rating;
    public DateTimeOffset Date { get; init; } = date;
    public Guid UserAccountId { get; set; }
}
