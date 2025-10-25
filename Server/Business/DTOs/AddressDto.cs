namespace Business.DTOs;

public class AddressDto(Guid addressId, string street, string city, string state, string zipCode, string country)
{
    public Guid AddressId { get; init; } = addressId;
    public string Street { get; private set; } = street;
    public string City { get; private set; } = city;
    public string State { get; private set; } = state;
    public string ZipCode { get; private set; } = zipCode;
    public string Country { get; private set; } = country;
}
