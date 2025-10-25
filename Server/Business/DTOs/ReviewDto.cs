namespace Business.DTOs;

public class ReviewDto(Guid reviewId, string comment, int rating, DateTimeOffset date)
{
    public Guid ReviewId { get; init; } = reviewId;
    public string Comment { get; private set; } = comment;
    public int Rating { get; private set; } = rating;
    public DateTimeOffset Date { get; private set; } = date;
}
