namespace Business.DTOs;

public class ClinicDto(Guid clinicId, string name, float rating, AddressDto address, IList<ReviewDto> reviews)
{
    public Guid ClinicId { get; set; } = clinicId;
    public string Name { get; set; } = name;
    public float Rating { get; set; } = rating;
    public AddressDto Address { get; set; } = address;
    public IList<ReviewDto> Reviews { get; set; } = reviews;
}