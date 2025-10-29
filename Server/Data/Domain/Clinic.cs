using Data.Domain.Account;

namespace Data.Domain;

public class Clinic
{
    public Guid ClinicId { get; init; }
    public string Name { get; private set; } = string.Empty;
    public float Rating { get; private set; } = 0f;

    public Guid AddressId { get; private set; }
    public Address Address { get; private set; } = null!;

    public ISet<Review> Reviews { get; private set; } = new HashSet<Review>();

    public Guid ClinicAdminAccountId { get; private set; }
    public ClinicAdminAccount ClinicAdminAccount { get; private set; } = null!;

    // EF Core
    private Clinic() { }

    public Clinic(string name, Guid addressId, Guid clinicAdminAccountId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        ClinicId = Guid.NewGuid();
        Name = name;
        AddressId = addressId;
        ClinicAdminAccountId = clinicAdminAccountId;
    }

    private void UpdateRating()
    {
        if (Reviews.Count == 0)
        {
            Rating = 0f;
            return;
        }

        Rating = (float)Reviews.Average(r => r.Rating);
    }

    public void AddReview(Review review)
    {
        ArgumentNullException.ThrowIfNull(review);

        Reviews.Add(review);
        UpdateRating();
    }
}
